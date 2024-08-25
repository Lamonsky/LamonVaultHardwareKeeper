using System;
using System.Text.Json.Serialization;

namespace Data.Computers.CreateEditVMs
{
    public class NetworkDeviceCreateEditVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("locationId")]
        public int? LocationId { get; set; }

        [JsonPropertyName("statusId")]
        public int? StatusId { get; set; }

        [JsonPropertyName("manufacturer")]
        public int? Manufacturer { get; set; }

        [JsonPropertyName("model")]
        public int? Model { get; set; }

        [JsonPropertyName("deviceType")]
        public int? DeviceType { get; set; }

        [JsonPropertyName("rack")]
        public int? Rack { get; set; }

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
