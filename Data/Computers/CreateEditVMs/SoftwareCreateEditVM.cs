using System;
using System.Text.Json.Serialization;

namespace Data.Computers.CreateEditVMs
{
    public class SoftwareCreateEditVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("locationId")]
        public int? LocationId { get; set; }

        [JsonPropertyName("publisher")]
        public int? Publisher { get; set; }

        [JsonPropertyName("users")]
        public int? Users { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("createdBy")]
        public int? CreatedBy { get; set; }

        [JsonPropertyName("modifiedAt")]
        public DateTime? ModifiedAt { get; set; }

        [JsonPropertyName("modifiedBy")]
        public int? ModifiedBy { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }
    }
}
