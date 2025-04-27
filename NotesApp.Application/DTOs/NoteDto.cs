using System.Text.Json.Serialization;

namespace NotesApp.Application.DTOs
{
    /// <summary>
    /// DTO class for representing a note.
    /// </summary>
    public class NoteDto
    {
        /// <summary>
        /// The unique identifier of the note.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

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
        /// Whether the note is archived or not.
        /// </summary>
        [JsonPropertyName("isArchived")]
        public bool IsArchived { get; set; }

        /// <summary>
        /// The date and time when the note was created.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The date and time when the note was last updated.
        /// </summary>
        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// The ID of the user to whom the note belongs.
        /// </summary>
        [JsonPropertyName("userId")]
        public string UserId { get; set; }
    }

}
