using System.Text.Json.Serialization;

namespace NotesApp.Application.DTOs
{
    public class UpdateNoteDto
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("isArchived")]
        public bool IsArchived { get; set; }
    }
}
