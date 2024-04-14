using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Computers.SelectVMs
{
    public class LocationVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        [JsonPropertyName("postalcode")]
        public string? PostalCode { get; set; }
        [JsonPropertyName("city")]
        public string? City { get; set; }
        [JsonPropertyName("country")]
        public string? Country { get; set; }
        [JsonPropertyName("buildingnumber")]
        public string? BuildingNumber { get; set; }
        [JsonPropertyName("roomnumber")]
        public string? RoomNumber { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }
}
