using System.Text.Json.Serialization;

namespace LogInDotNet.Model
{
    public class UserDTO
    {
        [JsonPropertyName("UserId")]
        public int UserId { get; set; }

        [JsonPropertyName("UserName")]
        public string UserName { get; set; } = null!;

        [JsonPropertyName("UserPsw")]
        public string UserPsw { get; set; } = null!;
        public override string ToString()
        {
            return string.Format("UserId: {0}, UserName: {1}, Password: {2}.", this.UserId, this.UserName, this.UserPsw);
        }
    }
}
