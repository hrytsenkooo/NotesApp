using Amazon.DynamoDBv2.DataModel;

namespace NotesApp.Infrastructure.Data.Models
{
    /// <summary>
    /// Represents a user item in DynamoDB storage.
    /// Maps to the "Users" table in DynamoDB.
    /// </summary>
    [DynamoDBTable("Users")]
    public class UserItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user item.
        /// This property is the hash key in DynamoDB.
        /// </summary>
        [DynamoDBHashKey("Id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// This property is used for querying by username in DynamoDB.
        /// </summary>
        [DynamoDBProperty("Username")]
        [DynamoDBGlobalSecondaryIndexHashKey("UsernameIndex")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// This property is used for querying by email in DynamoDB.
        /// </summary>
        [DynamoDBProperty("Email")]
        [DynamoDBGlobalSecondaryIndexHashKey("EmailIndex")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the user item.
        /// </summary>
        [DynamoDBProperty("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the last updated date of the user item.
        /// </summary>
        [DynamoDBProperty("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }

    }
}
