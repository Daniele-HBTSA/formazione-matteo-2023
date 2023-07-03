using System.Text.Json.Serialization;

namespace EnterpriseHierarchy.Models
{
    public class EnterpriseTree
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Code")]
        public string Code { get; set; } = null!;

        [JsonPropertyName("Balance")]
        public int Balance { get; set; }

        [JsonPropertyName("Selected")]
        public bool Selected { get; set; } = false;

        [JsonPropertyName("Children")]
        public List<EnterpriseTree> Children { get; set; } = new List<EnterpriseTree>();
    }
}
