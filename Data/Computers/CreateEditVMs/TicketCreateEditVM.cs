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
    public class TicketCreateEditVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("type")]
        public int? Type { get; set; }

        [JsonPropertyName("category")]
        public int? Category { get; set; }

        [JsonPropertyName("statusid")]
        public int? StatusId { get; set; }

        [JsonPropertyName("locationid")]
        public int? LocationId { get; set; }

        [JsonPropertyName("user")]
        public int? User { get; set; }

        [JsonPropertyName("owner")]
        public int? Owner { get; set; }

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
