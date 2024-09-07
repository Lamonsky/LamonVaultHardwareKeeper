using System.Text.Json.Serialization;

namespace WebInterface.Models
{
    public class LoginResponse
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; } = string.Empty;
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; } = string.Empty;
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
    }
}
