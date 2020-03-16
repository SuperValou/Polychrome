using System.Text.Json.Serialization;

namespace MetaVid.Tasks.ProbeDTO
{
    public class Stream
    {
        [JsonPropertyName("index")]
        public long Index { get; set; }

        // "video", "audio", "subtitle"
        [JsonPropertyName("codec_type")]
        public string CodecType { get; set; }

        // "h264", "aac", "dvd_subtitle"
        [JsonPropertyName("codec_name")]
        public string CodecName { get; set; }

        // "avc1", "mp4a", "mp4s"
        [JsonPropertyName("codec_tag_string")]
        public string CodecTagString { get; set; }

        [JsonPropertyName("start_time")]
        public string StartTime { get; set; }

        [JsonPropertyName("duration")]
        public string Duration { get; set; }

        [JsonPropertyName("nb_frames")]
        public string NbFrames { get; set; }

        [JsonPropertyName("bit_rate")]
        public string BitRate { get; set; }

        [JsonPropertyName("tags")]
        public StreamTags Tags { get; set; }

        // VIDEO

        [JsonPropertyName("width")]
        public long? Width { get; set; }

        [JsonPropertyName("height")]
        public long? Height { get; set; }

        // "30/1", "0/0" for other than video
        [JsonPropertyName("avg_frame_rate")]
        public string AvgFrameRate { get; set; }

        // AUDIO

        // number of channels
        [JsonPropertyName("channels")]
        public int? Channels { get; set; }

        // "stereo"
        [JsonPropertyName("channel_layout")]
        public string ChannelLayout { get; set; }

        // "48000"
        [JsonPropertyName("sample_rate")]
        public string SampleRate { get; set; }
    }
}