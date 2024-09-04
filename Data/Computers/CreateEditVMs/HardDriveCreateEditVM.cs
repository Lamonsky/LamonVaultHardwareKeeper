using System;
using System.Text.Json.Serialization;

namespace Data.Computers.CreateEditVMs
{
    public class HardDriveCreateEditVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("manufacturer")]
        public int? Manufacturer { get; set; }

        [JsonPropertyName("model")]
        public int? Model { get; set; }

        [JsonPropertyName("capacity")]
        public int? Capacity { get; set; }

        [JsonPropertyName("server")]
        public int? Server { get; set; }

        [JsonPropertyName("createdat")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("createdby")]
        public int? CreatedBy { get; set; }

        [JsonPropertyName("modifiedat")]
        public DateTime? ModifiedAt { get; set; }

        [JsonPropertyName("modifiedby")]
        public int? ModifiedBy { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }
    }
}
