using System.Text.Json.Serialization;

namespace NotesApp.Application.DTOs
{
    public class CreateUserDto
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
