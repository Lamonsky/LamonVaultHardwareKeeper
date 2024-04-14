using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Computers.SelectVMs
{
    public class SoftwaresVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("location")]
        public string? Location { get; set; }
        [JsonPropertyName("publisher")]
        public string? Publisher { get; set; }
        [JsonPropertyName("users")]
        public string? Users { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }

    }
}
