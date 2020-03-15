using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TmdbCrawler.Configurations.Tasks
{
    public class Crawl
    {
        [JsonPropertyName("source-files")]
        public List<string> SourceFiles { get; set; }

        [JsonPropertyName("source-root")]
        public Uri SourceRoot { get; set; }

        [JsonPropertyName("crawl-config")]
        public List<CrawlConfig> CrawlConfig { get; set; }

        [JsonPropertyName("output-folder")]
        public string OutputFolder { get; set; }

        [JsonPropertyName("skip-existing")]
        public bool SkipExisting { get; set; }

        [JsonPropertyName("keep-clean")]
        public bool KeepClean { get; set; }
    }
}