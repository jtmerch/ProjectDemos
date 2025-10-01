using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TSYSCertApi.Classes.JwtToken
{
    public class RefreshTokenRequest
    {
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
