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
        [JsonPropertyName("manufacturername")]
        public string? ManufacturerName { get; set; }
        [JsonPropertyName("location")]
        public string? Location { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("computertype")]
        public string? ComputerType { get; set; }
        [JsonPropertyName("computermodel")]
        public string? ComputerModel { get; set; }
        [JsonPropertyName("processor")]
        public string? Processor { get; set; }
        [JsonPropertyName("ram")]
        public string? Ram { get; set; }
        [JsonPropertyName("disk")]
        public string? Disk { get; set; }
        [JsonPropertyName("graphicscard")]
        public string? GraphicsCard { get; set; }
        [JsonPropertyName("operatingsystem")]
        public string? OperatingSystem { get; set; }
        [JsonPropertyName("serialnumber")]
        public string? SerialNumber { get; set; }
        [JsonPropertyName("inventorynumber")]
        public string? InventoryNumber { get; set; }
        [JsonPropertyName("user")]
        public string? User { get; set; }

    }
}
