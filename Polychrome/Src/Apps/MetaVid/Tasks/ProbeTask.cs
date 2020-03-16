using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore.Tasks;
using Kernel;
using MediaDatabase.Service;
using MediaDatabase.Service.DTOs;
using MetaVid.Configurations;
using MetaVid.Tasks.ProbeDTO;

namespace MetaVid.Tasks
{
    public class ProbeTask : ITask
    {
        private const string Pattern = "*.mp4";
        private const string FfProbeCommand =  "-v error -print_format json -show_format -show_streams \"{0}\" > \"{1}\""; // {0} is input path, {1} is output path

        private readonly ProbeTaskSetup _setup;
        private readonly ILogger _logger;
        private readonly string _workingDirectory;
        private readonly IMediaDatabaseService _mediaDatabaseService;

        public ProbeTask(ProbeTaskSetup setup, ILogger logger, string workingDirectory, IMediaDatabaseService mediaDatabaseService)
        {
            _setup = setup ?? throw new ArgumentNullException(nameof(setup));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _workingDirectory = workingDirectory ?? throw new ArgumentNullException(nameof(workingDirectory));
            _mediaDatabaseService = mediaDatabaseService ?? throw new ArgumentNullException(nameof(mediaDatabaseService));
        }
        
        public async Task Execute()
        {
            _logger.Info($"Probing {_setup.SourceFolder}...");

            ILogger ffprobeLogger = _logger.CreateSubLogger("FFProbe");
            int probedFileCount = 0;
            foreach (var fileToProbe in Directory.EnumerateFiles(_setup.SourceFolder, Pattern, SearchOption.AllDirectories))
            {
                string outputFileName = Path.GetFileNameWithoutExtension(fileToProbe);
                string ffprobeOutputFilePath = Path.Combine(_workingDirectory, outputFileName + "-raw.json");

                string args = string.Format(FfProbeCommand, fileToProbe, ffprobeOutputFilePath);

                bool success;
                using (var process = new ExeProcess(ffprobeLogger, _setup.FfProbePath, args))
                {
                    success = await process.Run();
                }

                if (!success)
                {
                    _logger.Debug($"Skipping '{fileToProbe}' because of error.");
                    continue;
                }

                string mediaId = _mediaDatabaseService.GetMediaId(fileToProbe);

                ProbedData probedData;
                await using (var reader = File.OpenRead(ffprobeOutputFilePath))
                {
                    probedData = await JsonSerializer.DeserializeAsync<ProbedData>(reader);
                }

                MediaInfo mediaInfo = _mediaDatabaseService.GetMediaInfo(mediaId);

                mediaInfo = UpdateMediaInfoWithProbedData(mediaInfo, probedData);

                _mediaDatabaseService.Update(mediaId, mediaInfo);

                _logger.Debug($"Probed '{outputFileName}'");
                probedFileCount++;
            }

            _logger.Info($"Probed {probedFileCount} files.");
        }

        private MediaInfo UpdateMediaInfoWithProbedData(MediaInfo mediaInfoSource, ProbedData probedData)
        {
            MediaInfo mediaInfo = mediaInfoSource.Clone();

            mediaInfo.Filename = probedData.Format.Filename;
            mediaInfo.StartTime = float.Parse(probedData.Format.StartTime);
            mediaInfo.Duration = float.Parse(probedData.Format.Duration);

            // metadata
            mediaInfo.FileSize = long.Parse(probedData.Format.Size);
            mediaInfo.CreatedOn = DateTime.Parse(probedData.Format.Tags.CreationTime);
            mediaInfo.Encoder = probedData.Format.Tags.Encoder;
            mediaInfo.FormatLongName = probedData.Format.FormatLongName;

            // streams
            mediaInfo.NbStreams = probedData.Format.NbStreams;

            // video
            mediaInfo.VideoStreams.Clear();
            foreach (var probedVideoStream in probedData.Streams.Where(s => s.CodecType == "video"))
            {
                var videoStream = new VideoStream()
                {
                    Index = probedVideoStream.Index,
                    StartTime = float.Parse(probedVideoStream.StartTime),
                    Duration = float.Parse(probedVideoStream.Duration),
                    CodecName = probedVideoStream.CodecName,
                    CodecTag = probedVideoStream.CodecTagString,

                    Width = probedVideoStream.Width.Value,
                    Height = probedVideoStream.Height.Value,
                    NbFrames = int.Parse(probedVideoStream.NbFrames),
                    Fps = probedVideoStream.AvgFrameRate
                };

                mediaInfo.VideoStreams.Add(videoStream);
            }

            // audio
            mediaInfo.AudioStreams.Clear();
            foreach (var probedAudioStream in probedData.Streams.Where(s => s.CodecType == "audio"))
            {
                var audioStream = new AudioStream()
                {
                    Index = probedAudioStream.Index,
                    StartTime = float.Parse(probedAudioStream.StartTime),
                    Duration = float.Parse(probedAudioStream.Duration),
                    CodecName = probedAudioStream.CodecName,
                    CodecTag = probedAudioStream.CodecTagString,

                    Language = probedAudioStream.Tags.Language,
                    NbChannels = probedAudioStream.Channels.Value,
                    ChannelLayoutName = probedAudioStream.ChannelLayout,
                    SampleRate = int.Parse(probedAudioStream.SampleRate)
                };

                mediaInfo.AudioStreams.Add(audioStream);
            }

            // subtitles
            mediaInfo.SubtitleStreams.Clear();
            foreach (var probedSubtitleStream in probedData.Streams.Where(s => s.CodecType == "subtitle"))
            {
                var subtitleStream = new SubtitleStream()
                {
                    Index = probedSubtitleStream.Index,
                    StartTime = float.Parse(probedSubtitleStream.StartTime),
                    Duration = float.Parse(probedSubtitleStream.Duration),
                    CodecName = probedSubtitleStream.CodecName,
                    CodecTag = probedSubtitleStream.CodecTagString,

                    Language = probedSubtitleStream.Tags.Language
                };

                mediaInfo.SubtitleStreams.Add(subtitleStream);
            }

            return mediaInfo;
        }
    }
}
