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
    public class SimComponentCreateEditVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("manufacturer")]
        public int Manufacturer { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("createdat")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("createdby")]
        public int CreatedBy { get; set; }

        [JsonPropertyName("modifiedat")]
        public DateTime ModifiedAt { get; set; }

        [JsonPropertyName("modifiedby")]
        public int ModifiedBy { get; set; }
    }
}
