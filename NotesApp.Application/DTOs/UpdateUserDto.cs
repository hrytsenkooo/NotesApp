using System.Text.Json.Serialization;

namespace NotesApp.Application.DTOs
{
    /// <summary>
    /// DTO class for updating an existing user.
    /// </summary>
    public class UpdateUserDto
    {
        /// <summary>
        /// The username of the user.
        /// </summary>
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        /// <summary>
        /// The email of the user.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
