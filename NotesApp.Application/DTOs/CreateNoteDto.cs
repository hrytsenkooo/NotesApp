using System.Text.Json.Serialization;

namespace NotesApp.Application.DTOs
{
    /// <summary>
    /// DTO class for creating a new note.
    /// </summary>
    public class CreateNoteDto
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
        /// The ID of the user to whom the note belongs.
        /// </summary>
        [JsonPropertyName("userId")]
        public string UserId { get; set; }
    }
}
