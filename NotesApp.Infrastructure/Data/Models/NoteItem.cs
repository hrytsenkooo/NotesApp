using Amazon.DynamoDBv2.DataModel;

namespace NotesApp.Infrastructure.Data.Models
{
    /// <summary>
    /// Represents a note item in DynamoDB storage.
    /// Maps to the "Notes" table in DynamoDB.
    /// </summary>
    [DynamoDBTable("Notes")]
    public class NoteItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the note item.
        /// This property is the hash key in DynamoDB.
        /// </summary>
        [DynamoDBHashKey("Id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the note.
        /// </summary>
        [DynamoDBProperty("Title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the content of the note.
        /// </summary>
        [DynamoDBProperty("Content")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the archived status of the note.
        /// </summary>
        [DynamoDBProperty("IsArchived")]
        public bool isArchived { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the note.
        /// </summary>
        [DynamoDBProperty("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last updated date of the note.
        /// </summary>
        [DynamoDBProperty("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user who created the note.
        /// This property is used for querying by user in DynamoDB.
        /// </summary>
        [DynamoDBProperty("UserId")]
        [DynamoDBGlobalSecondaryIndexHashKey("UserIdIndex")]
        public string UserId { get; set; }

    }
}
