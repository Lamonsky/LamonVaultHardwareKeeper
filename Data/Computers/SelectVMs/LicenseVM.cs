using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Computers.SelectVMs
{
    public class LicenseVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("software")]
        public string? Software { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("licensetype")]
        public string? LicenseType { get; set; }
        [JsonPropertyName("publisher")]
        public string? Publisher { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("location")]
        public string? Location { get; set; }
        [JsonPropertyName("serialnumber")]
        public string? SerialNumber { get; set; }
        [JsonPropertyName("inventorynumber")]
        public string? InventoryNumber { get; set; }
        [JsonPropertyName("expirydate")]
        public DateTime? ExpiryDate { get; set; }
        [JsonPropertyName("user")]
        public string? User { get; set; }
    }
}
