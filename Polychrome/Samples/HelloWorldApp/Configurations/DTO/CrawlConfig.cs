﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelloWorldApp.Configurations.DTO
{
    public class CrawlConfig
    {
        [JsonPropertyName("skip")]
        public bool Skip { get; set; }

        [JsonPropertyName("resource")]
        public string Resource { get; set; }

        [JsonPropertyName("append")]
        public string Append { get; set; }

        [JsonPropertyName("filters")]
        public List<FilterElement> Filters { get; set; }
    }
}