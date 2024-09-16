using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Computers.SelectVMs
{
    public class RackCabinetVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("location")]
        public string? Location { get; set; }
        [JsonPropertyName("cabinettype")]
        public string? CabinetType { get; set; }
        [JsonPropertyName("manufacturer")]
        public string? Manufacturer { get; set; }
        [JsonPropertyName("model")]
        public string? Model { get; set; }
        [JsonPropertyName("height")]
        public string? Height { get; set; }
        [JsonPropertyName("serialnumber")]
        public string? SerialNumber { get; set; }
        [JsonPropertyName("inventorynumber")]
        public string? InventoryNumber { get; set; }
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
