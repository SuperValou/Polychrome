using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelloWorldApp.Configurations.DTO
{
    public class Download
    {
        [JsonPropertyName("source-root")]
        public Uri SourceRoot { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("ids")]
        public List<string> Ids { get; set; }

        [JsonPropertyName("source-filename")]
        public string SourceFilename { get; set; }

        [JsonPropertyName("output-filename")]
        public string OutputFilename { get; set; }

        [JsonPropertyName("output-folder")]
        public string OutputFolder { get; set; }

        [JsonPropertyName("skip-existing")]
        public bool SkipExisting { get; set; }

        [JsonPropertyName("keep-clean")]
        public bool KeepClean { get; set; }
    }
}