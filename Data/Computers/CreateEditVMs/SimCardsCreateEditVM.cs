using System;
using System.Text.Json.Serialization;

namespace Data.Computers.CreateEditVMs
{
    public class SimCardsCreateEditVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("pinCode")]
        public string? PinCode { get; set; }

        [JsonPropertyName("pukCode")]
        public string? PukCode { get; set; }

        [JsonPropertyName("component")]
        public int? Component { get; set; }

        [JsonPropertyName("serialNumber")]
        public string? SerialNumber { get; set; }

        [JsonPropertyName("inventoryNumber")]
        public string? InventoryNumber { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("statusId")]
        public int? StatusId { get; set; }

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
