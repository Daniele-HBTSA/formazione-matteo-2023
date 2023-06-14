using System.Text.Json.Serialization;

namespace FinanceApp.Models
{
    public class JwtDTO
    {
        public string token { get; set; }

        public JwtDTO(string token) 
        {
            this.token = token;
        }
    }
}
