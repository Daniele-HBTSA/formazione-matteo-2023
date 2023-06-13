using System.Text.Json.Serialization;

namespace FinanceApp.Models
{
    public class JwtDTO
    {
        [JsonPropertyName("tokenPersonale")]
        public string token { get; set; }

        public JwtDTO(string token) 
        {
            this.token = token;
        }
    }
}
