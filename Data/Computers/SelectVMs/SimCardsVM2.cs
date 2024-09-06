using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Computers.SelectVMs
{
    public class SimCardsVM2
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("pincode")]
        public string? PinCode { get; set; }
        [JsonPropertyName("pukcode")]
        public string? PukCode { get; set; }
        [JsonPropertyName("component")]
        public string? Component { get; set; }
        [JsonPropertyName("serialnumber")]
        public string? SerialNumber { get; set; }
        [JsonPropertyName("inventorynumber")]
        public string? InventoryNumber { get; set; }
        [JsonPropertyName("phonenumber")]
        public string? PhoneNumber { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("users")]
        public string? Users { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("createdBy")]
        public string? CreatedBy { get; set; }

        [JsonPropertyName("modifiedAt")]
        public DateTime? ModifiedAt { get; set; }

        [JsonPropertyName("modifiedBy")]
        public string? ModifiedBy { get; set; }
    }
}
