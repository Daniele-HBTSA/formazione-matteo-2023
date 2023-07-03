namespace SicurezzaBackEnd.Dtos
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
