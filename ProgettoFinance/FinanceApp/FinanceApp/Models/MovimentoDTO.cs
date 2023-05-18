using System.Text.Json.Serialization;

namespace FinanceApp.Models
{
    public class MovimentoDTO
    {
        [JsonPropertyName("CodiceMovimento")]
        public int? CodiceMovimento { get; set; }

        [JsonPropertyName("ValoreMovimento")]
        public int? ValoreMovimento { get; set; } = null!;

        public override string ToString()
        {
            return string.Format("Codice movimento: {0}, Valore movimento: {1}",
                this.CodiceMovimento, this.ValoreMovimento);
        }
    }
}
