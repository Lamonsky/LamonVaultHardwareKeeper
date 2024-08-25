using System;
using System.Text.Json.Serialization;

namespace Data.Computers.CreateEditVMs
{
    public class UserCreateEditVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("usersname")]
        public string? Usersname { get; set; }

        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }

        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("locationId")]
        public int? LocationId { get; set; }

        [JsonPropertyName("isActive")]
        public bool? IsActive { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("phone1")]
        public string? Phone1 { get; set; }

        [JsonPropertyName("phone2")]
        public string? Phone2 { get; set; }

        [JsonPropertyName("internalNumber")]
        public string? InternalNumber { get; set; }

        [JsonPropertyName("position")]
        public int? Position { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("createdBy")]
        public int? CreatedBy { get; set; }

        [JsonPropertyName("modifiedAt")]
        public DateTime? ModifiedAt { get; set; }

        [JsonPropertyName("modifiedBy")]
        public int? ModifiedBy { get; set; }
    }
}
