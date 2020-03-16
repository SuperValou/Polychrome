using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MediaDatabase.Service.DTOs
{
    public class MediaInfo
    {
        [JsonPropertyName("file-path")]
        public string FilePath { get; set; }
        
        [JsonPropertyName("start-time")]
        public float StartTime { get; set; }

        [JsonPropertyName("duration")]
        public float Duration { get; set; }


        [JsonPropertyName("nb-streams")]
        public int NbStreams { get; set; }

        [JsonPropertyName("video-streams")]
        public List<VideoStream> VideoStreams { get; set; } = new List<VideoStream>();

        [JsonPropertyName("audio-streams")]
        public List<AudioStream> AudioStreams { get; set; } = new List<AudioStream>();

        [JsonPropertyName("subtitle-streams")]
        public List<SubtitleStream> SubtitleStreams { get; set; } = new List<SubtitleStream>();


        [JsonPropertyName("file-size")]
        public long FileSize { get; set; }
        
        [JsonPropertyName("created-on")]
        public DateTime CreatedOn { get; set; }

        [JsonPropertyName("encoder")]
        public string Encoder { get; set; }

        [JsonPropertyName("format-name")]
        public string FormatLongName { get; set; }

        public MediaInfo Clone()
        {
            return JsonSerializer.Deserialize<MediaInfo>(JsonSerializer.Serialize(this));
        }
    }
}