using System.Text.Json.Serialization;

namespace NotesApp.Application.DTOs
{
    /// <summary>
    /// DTO class for representing a user.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// The unique identifier of the user.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The username of the user.
        /// </summary>
        [JsonPropertyName("username")]
        public string Username { get; set; }

        /// <summary>
        /// The email of the user.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }

        /// <summary>
        /// The date and time when the user was created.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The date and time when the user was last updated.
        /// </summary>
        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
