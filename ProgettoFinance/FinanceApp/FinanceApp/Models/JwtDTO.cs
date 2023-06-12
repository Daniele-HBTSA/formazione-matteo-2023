namespace FinanceApp.Models
{
    public class JwtDTO
    {
        public string token;
        public JwtDTO(string token) 
        {
            this.token = token;
        }
    }
}
