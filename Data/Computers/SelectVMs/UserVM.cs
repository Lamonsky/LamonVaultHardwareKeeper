using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Data.Computers.SelectVMs
{
    public class UserVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("username")]
        public string? Usersname { get; set; }
        [JsonPropertyName("lastname")]
        public string? LastName { get; set; }
        [JsonPropertyName("firstname")]
        public string? FirstName { get; set; }
        [JsonPropertyName("password")]
        public string? Password { get; set; }
        [JsonPropertyName("location")]
        public string? Location { get; set; }
        [JsonPropertyName("locationid")]
        public int LocationId { get; set; }
        [JsonPropertyName("isactive")]
        public bool? IsActive { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("phone1")]
        public string? Phone1 { get; set; }
        [JsonPropertyName("phone2")]
        public string? Phone2 { get; set; }
        [JsonPropertyName("internalnumber")]
        public string? InternalNumber { get; set; }
        [JsonPropertyName("position")]
        public string? Position { get; set; }
    }
}
