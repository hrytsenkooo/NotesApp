using System.Text.Json.Serialization;

namespace NotesApp.Application.DTOs
{
    public class UpdateUserDto
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
