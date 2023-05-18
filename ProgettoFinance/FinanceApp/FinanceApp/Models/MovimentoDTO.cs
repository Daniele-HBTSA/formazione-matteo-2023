using System.Text.Json.Serialization;

namespace FinanceApp.Models
{
    public class MovimentoDTO
    {
        [JsonPropertyName("IdAzienda")]
        public int IdAzienda { get; set; }

        [JsonPropertyName("ValoreMovimento")]
        public int ValoreMovimento { get; set; }

        public override string ToString()
        {
            return string.Format("Codice movimento: {0}, Valore movimento: {1}",
                this.IdAzienda, this.ValoreMovimento);
        }
    }
}
