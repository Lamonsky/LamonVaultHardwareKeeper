using System;
using System.Text.Json.Serialization;

namespace Data.Computers.CreateEditVMs
{
    public class LicenseCreateEditVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("software")]
        public int? Software { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("licensetype")]
        public int? LicenseType { get; set; }

        [JsonPropertyName("publisher")]
        public int? Publisher { get; set; }

        [JsonPropertyName("statusid")]
        public int? StatusId { get; set; }

        [JsonPropertyName("locationid")]
        public int? LocationId { get; set; }

        [JsonPropertyName("serialnumber")]
        public string? SerialNumber { get; set; }

        [JsonPropertyName("inventorynumber")]
        public string? InventoryNumber { get; set; }

        [JsonPropertyName("expirydate")]
        public DateTime? ExpiryDate { get; set; }

        [JsonPropertyName("users")]
        public int? Users { get; set; }

        [JsonPropertyName("createdat")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("createdby")]
        public int? CreatedBy { get; set; }

        [JsonPropertyName("modifiedat")]
        public DateTime? ModifiedAt { get; set; }

        [JsonPropertyName("modifiedby")]
        public int? ModifiedBy { get; set; }
    }
}
