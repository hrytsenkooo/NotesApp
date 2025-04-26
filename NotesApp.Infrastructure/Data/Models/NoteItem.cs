using Amazon.DynamoDBv2.DataModel;

namespace NotesApp.Infrastructure.Data.Models
{
    [DynamoDBTable("Notes")]
    public class NoteItem
    {
        [DynamoDBHashKey("Id")]
        public string Id { get; set; }

        [DynamoDBProperty("Title")]
        public string Title { get; set; }

        [DynamoDBProperty("Content")]
        public string Content { get; set; }

        [DynamoDBProperty("IsArchived")]
        public bool isArchived { get; set; }

        [DynamoDBProperty("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [DynamoDBProperty("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }

        [DynamoDBProperty("UserId")]
        [DynamoDBGlobalSecondaryIndexHashKey("UserIdIndex")]
        public string UserId { get; set; }

    }
}
