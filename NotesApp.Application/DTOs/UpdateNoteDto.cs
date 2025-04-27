using System.Text.Json.Serialization;

namespace NotesApp.Application.DTOs
{
    /// <summary>
    /// DTO class for updating an existing note.
    /// </summary>
    public class UpdateNoteDto
    {
        /// <summary>
        /// The title of the note.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// The content of the note.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }

        /// <summary>
        /// Whether the note should be archived.
        /// </summary>
        [JsonPropertyName("isArchived")]
        public bool IsArchived { get; set; }
    }
}
