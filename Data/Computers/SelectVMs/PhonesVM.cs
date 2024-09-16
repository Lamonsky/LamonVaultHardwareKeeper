using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Computers.SelectVMs
{
    public class PhonesVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("location")]
        public string? Location { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("phonetype")]
        public string? PhoneType { get; set; }
        [JsonPropertyName("manufacturer")]
        public string? Manufacturer { get; set; }
        [JsonPropertyName("model")]
        public string? Model { get; set; }
        [JsonPropertyName("serialnumber")]
        public string? SerialNumber { get; set; }
        [JsonPropertyName("inventorynumber")]
        public string? InventoryNumber { get; set; }
        [JsonPropertyName("simcard1")]
        public string? SimCard1 { get; set; }
        [JsonPropertyName("simcard2")]
        public string? SimCard2 { get; set; }
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
        public string Template { get; set; }
    }
}
