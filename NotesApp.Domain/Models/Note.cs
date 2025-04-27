namespace NotesApp.Domain.Models
{
    /// <summary>
    /// Represents a note created by a user.
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Gets or sets the unique identifier for the note.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the note.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the content of the note.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the archived status of the note.
        /// </summary>
        public bool IsArchived { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the note.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last updated date of the note.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the note.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the user who created the note.
        /// </summary>
        public User User { get; set; }
    }
}
