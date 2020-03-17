using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Tasks;
using Kernel;
using MediaDatabase.Service;
using MediaDatabase.Service.DTOs;
using SwapFusion.Configurations;

namespace SwapFusion.Tasks
{
    public class GenerateSwapsTask : WorkingDirectoryTask
    {
        private readonly GenerateSwapsTaskSetup _setup;
        private readonly IMediaDatabaseService _mediaDatabaseService;
        private readonly Random _random = new Random();

        public GenerateSwapsTask(ILogger logger, GenerateSwapsTaskSetup setup, IMediaDatabaseService mediaDatabaseService)
            : base(logger)
        {
            _setup = setup ?? throw new ArgumentNullException(nameof(setup));
            _mediaDatabaseService = mediaDatabaseService ?? throw new ArgumentNullException(nameof(mediaDatabaseService));
        }

        public override async Task Execute()
        {
            List<string> mediaIds = new List<string>();
            if (_setup.UseMedia == null || _setup.UseMedia.Count == 0)
            {
                ICollection<string> availableMediaIds = await _mediaDatabaseService.GetAllMediaIds();
                mediaIds.AddRange(availableMediaIds);
            }
            else
            {
                mediaIds.AddRange(_setup.UseMedia);
            }

            ILogger ffmpegLogger = Logger.CreateSubLogger("FFMpeg.exe");
            for (int i = 0; i < _setup.CoupleCount; i++)
            {
                // choose video
                int videoMediaIndex = _random.Next(0, mediaIds.Count);
                string videoMediaId = mediaIds[videoMediaIndex];
                MediaInfo videoMediaInfo = await _mediaDatabaseService.GetMediaInfo(videoMediaId);
                
                VideoStream videoStream = videoMediaInfo.VideoStreams.FirstOrDefault();
                if (videoStream == null)
                {
                    Logger.Warn($"{videoMediaId} has no video stream.");
                    continue;
                }

                int videoMinLimit = (int) Math.Ceiling(videoStream.StartTime);
                int videoMaxLimit = (int) Math.Floor(videoStream.Duration) - _setup.SwapDuration;

                if (videoMaxLimit < videoMinLimit)
                {
                    Logger.Warn($"{videoMediaId} is too short to create a {_setup.SwapDuration}s swap.");
                    continue;
                }
                
                // choose audio
                int audioMediaIndex = _random.Next(0, mediaIds.Count);
                string audioMediaId = mediaIds[audioMediaIndex];
                MediaInfo audioMediaInfo = await _mediaDatabaseService.GetMediaInfo(audioMediaId);

                AudioStream audioStream =  audioMediaInfo.AudioStreams.FirstOrDefault(stream => stream.Language == _setup.AudioLanguage);
                if (audioStream == null)
                {
                    Logger.Warn($"{audioMediaId} has no audio stream with '{_setup.AudioLanguage}' language available.");
                    continue;
                }

                int audioMinLimit = (int)Math.Ceiling(audioStream.StartTime);
                int audioMaxLimit = (int)Math.Floor(audioStream.Duration) - _setup.SwapDuration;

                // generate swaps for this couple
                for (int j = 0; j < _setup.SwapsPerCouple; j++)
                {
                    int videoStartTime = _random.Next(videoMinLimit, videoMaxLimit);
                    
                    TimeSpan videoTimeSpan = TimeSpan.FromSeconds(videoStartTime);
                    string videoStartTimestamp = $"{videoTimeSpan.Hours:D2}:{videoTimeSpan.Minutes:D2}:{videoTimeSpan.Seconds:D2}";

                    int audioStartTime = _random.Next(audioMinLimit, audioMaxLimit);

                    TimeSpan audioTimeSpan = TimeSpan.FromSeconds(audioStartTime);
                    string audioStartTimestamp = $"{audioTimeSpan.Hours:D2}:{audioTimeSpan.Minutes:D2}:{audioTimeSpan.Seconds:D2}";

                    string outputFileName = $"{videoMediaId}_{videoStartTimestamp.Replace(':', '-')}_{audioMediaId}_{audioStartTimestamp.Replace(':', '-')}_{j}.mp4";
                    string outputFilePath = Path.Combine(WorkingDirectory, outputFileName);

                    string args = GetFfmpegArgs(
                        videoMediaInfo.FilePath, videoStream.Index, videoStartTimestamp,
                        audioMediaInfo.FilePath, audioStream.Index, audioStartTimestamp,
                        _setup.SwapDuration, outputFilePath);

                    using (var process = new ExeProcess(ffmpegLogger, _setup.FfmpegPath, args))
                    {
                        await process.Run();
                    }
                }
            }
        }

        private string GetFfmpegArgs(
            string videoFilePath, int videoStreamIndex, string videoStartTimestamp,
            string audioFilePath, int audioStreamIndex, string audioStartTimestamp,
            int duration, string outputFilePath)
        {
            string args = $"-v error " +
                          $"-ss {videoStartTimestamp} -t {duration} -i \"{videoFilePath}\" " +
                          $"-ss {audioStartTimestamp} -t {duration} -i \"{audioFilePath}\" " +
                          $"-map 0:{videoStreamIndex} -c:v copy " + // video stream
                          $"-map 1:{audioStreamIndex} -c:a copy " + // audio stream
                          $"\"{outputFilePath}\""; // output

            return args;
        }
    }
}