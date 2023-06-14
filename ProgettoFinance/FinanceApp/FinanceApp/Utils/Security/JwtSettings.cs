namespace FinanceApp.Utils.Security
{
    public class JwtSettings
    {
        public string Secret { get; set; } = null!;
        public int Expire { get; set; }
        public int Refresh { get; set; }
    }
}
