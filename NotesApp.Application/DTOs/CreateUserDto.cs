using System.Text.Json.Serialization;

namespace NotesApp.Application.DTOs
{
    /// <summary>
    /// DTO class for creating a new user.
    /// </summary>
    public class CreateUserDto
    {
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
    }
}
