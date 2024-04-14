using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Computers.SelectVMs
{
    public class GroupVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("users")]
        public string? Users { get; set; }
        [JsonPropertyName("groupname")]
        public string? GroupName { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }
}
