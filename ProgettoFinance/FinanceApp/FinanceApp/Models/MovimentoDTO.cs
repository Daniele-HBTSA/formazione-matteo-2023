using System.Text.Json.Serialization;

namespace FinanceApp.Models
{
    public class MovimentoDTO
    {
        [JsonPropertyName("IdMovimento")]
        public int? IdMovimento { get; set; }

        [JsonPropertyName("IdAzienda")]
        public int IdAzienda { get; set; }

        [JsonPropertyName("ValoreMovimento")]
        public int ValoreMovimento { get; set; }

        public override string ToString()
        {
            return string.Format("Id Movimento: {0}, Id Azienda: {1}, Valore movimento: {2}",
                this.IdMovimento, this.IdAzienda, this.ValoreMovimento);
        }
    }
}
