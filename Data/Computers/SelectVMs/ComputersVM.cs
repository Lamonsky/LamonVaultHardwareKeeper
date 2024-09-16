using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Computers.SelectVMs
{
    public class ComputersVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("manufacturerName")]
        public string? ManufacturerName { get; set; }
        [JsonPropertyName("location")]
        public string? Location { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("computerType")]
        public string? ComputerType { get; set; }
        [JsonPropertyName("computerModel")]
        public string? ComputerModel { get; set; }
        [JsonPropertyName("processor")]
        public string? Processor { get; set; }
        [JsonPropertyName("ram")]
        public string? Ram { get; set; }
        [JsonPropertyName("disk")]
        public string? Disk { get; set; }
        [JsonPropertyName("graphicsCard")]
        public string? GraphicsCard { get; set; }
        [JsonPropertyName("operatingSystem")]
        public string? OperatingSystem { get; set; }
        [JsonPropertyName("serialNumber")]
        public string? SerialNumber { get; set; }
        [JsonPropertyName("inventoryNumber")]
        public string? InventoryNumber { get; set; }
        [JsonPropertyName("user")]
        public string? User { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("createdBy")]
        public string? CreatedBy { get; set; }

        [JsonPropertyName("modifiedAt")]
        public DateTime? ModifiedAt { get; set; }

        [JsonPropertyName("modifiedBy")]
        public string? ModifiedBy { get; set; }
        [JsonPropertyName("teamplate")]
        public string? Template { get; set; }

        

    }
}
