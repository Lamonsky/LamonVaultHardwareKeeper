using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Data.Computers.CreateEditVMs
{
    public class ServerCreateEditVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("locationid")]
        public int? LocationId { get; set; }

        [JsonPropertyName("statusid")]
        public int? StatusId { get; set; }

        [JsonPropertyName("manufacturer")]
        public int? Manufacturer { get; set; }

        [JsonPropertyName("model")]
        public int? Model { get; set; }

        [JsonPropertyName("processor")]
        public string? Processor { get; set; }

        [JsonPropertyName("ram")]
        public string? Ram { get; set; }

        [JsonPropertyName("operatingsystem")]
        public int? OperatingSystem { get; set; }

        [JsonPropertyName("serialnumber")]
        public string? SerialNumber { get; set; }

        [JsonPropertyName("inventorynumber")]
        public string? InventoryNumber { get; set; }

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
