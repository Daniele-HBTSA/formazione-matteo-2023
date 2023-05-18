using System.Text.Json.Serialization;

namespace FinanceApp.Models
{
    public class AziendaDTO
    {
        [JsonPropertyName("AccountAzienda")]
        public string AccountAzienda { get; set; } = null!;

        [JsonPropertyName("PswAzienda")]
        public string PswAzienda { get; set; } = null!;

        [JsonPropertyName("NomeAzienda")]
        public string NomeAzienda { get; set; } = null!;

        [JsonPropertyName("CapitaleAzienda")]
        public int? CapitaleAzienda { get; set; }

        public override string ToString()
        {
            return string.Format("Account azienda: {0}, Psw azienda: {1}, Nome azienda: {2}, Capitale azienda: {3}.", 
                this.AccountAzienda, this.PswAzienda, this.NomeAzienda, this.CapitaleAzienda);
        }
    }
}
