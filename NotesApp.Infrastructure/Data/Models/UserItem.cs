using Amazon.DynamoDBv2.DataModel;

namespace NotesApp.Infrastructure.Data.Models
{
    [DynamoDBTable("Users")]
    public class UserItem
    {
        [DynamoDBHashKey("Id")]
        public string Id { get; set; }

        [DynamoDBProperty("Username")]
        [DynamoDBGlobalSecondaryIndexHashKey("UsernameIndex")]
        public string UserName { get; set; }

        [DynamoDBProperty("Email")]
        [DynamoDBGlobalSecondaryIndexHashKey("EmailIndex")]
        public string Email { get; set; }

        [DynamoDBProperty("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [DynamoDBProperty("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }

    }
}
