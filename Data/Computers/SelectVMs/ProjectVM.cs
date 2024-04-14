using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Computers.SelectVMs
{
    public class ProjectVM
    {
        [JsonPropertyName("name")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("priority")]
        public string? Priority { get; set; }
        [JsonPropertyName("code")]
        public string? Code { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("projecttype")]
        public string? ProjectType { get; set; }
        [JsonPropertyName("user")]
        public string? User { get; set; }
        [JsonPropertyName("plannedstartdate")]
        public DateTime? PlannedStartDate { get; set; }
        [JsonPropertyName("plannedenddate")]
        public DateTime? PlannedEndDate { get; set; }
        [JsonPropertyName("actualstartdate")]
        public DateTime? ActualStartDate { get; set; }
        [JsonPropertyName("actualenddate")]
        public DateTime? ActualEndDate { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("comment")]
        public string? Comment { get; set; }
    }
}
