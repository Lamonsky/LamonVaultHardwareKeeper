﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Computers.SelectVMs
{
    public class LogVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("users")]
        public string Users { get; set; }

        [JsonPropertyName("logdate")]
        public DateTime LogDate { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("createdat")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("createdby")]
        public string CreatedBy { get; set; }
    }
}
