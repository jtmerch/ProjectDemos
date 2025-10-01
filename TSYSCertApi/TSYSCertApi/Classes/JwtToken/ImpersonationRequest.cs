using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TSYSCertApi.Classes.JwtToken
{
    public class ImpersonationRequest
    {

        [JsonPropertyName("username")]
        public string UserName { get; set; }
    }
}
