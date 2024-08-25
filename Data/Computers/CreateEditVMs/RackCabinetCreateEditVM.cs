using System;
using System.Text.Json.Serialization;

namespace Data.Computers.CreateEditVMs
{
    public class RackCabinetCreateEditVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("statusId")]
        public int? StatusId { get; set; }

        [JsonPropertyName("locationId")]
        public int? LocationId { get; set; }

        [JsonPropertyName("cabinetType")]
        public int? CabinetType { get; set; }

        [JsonPropertyName("manufacturer")]
        public int? Manufacturer { get; set; }

        [JsonPropertyName("model")]
        public int? Model { get; set; }

        [JsonPropertyName("height")]
        public int? Height { get; set; }

        [JsonPropertyName("serialNumber")]
        public string? SerialNumber { get; set; }

        [JsonPropertyName("inventoryNumber")]
        public string? InventoryNumber { get; set; }

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
    }
}
