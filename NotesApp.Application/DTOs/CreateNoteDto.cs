using System.Text.Json.Serialization;

namespace NotesApp.Application.DTOs
{
    public class CreateNoteDto
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }
    }
}
