using NotesApp.Application.DTOs;
using NotesApp.Domain.Models;
using System.Text.Json;

namespace NotesApp.Application.Mappers
{
    /// <summary>
    /// Provides mapping methods to convert between DTOs and domain models.
    /// </summary>
    public static class DtoMappers
    {
        /// <summary>
        /// Converts a <see cref="User"/> object to a <see cref="UserDto"/>.
        /// </summary>
        /// <param name="user">The user object to be mapped.</param>
        /// <returns>A <see cref="UserDto"/> object containing the user information.</returns>
        public static UserDto ToUserDto(this User user)
        {
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        /// <summary>
        /// Converts a <see cref="CreateUserDto"/> to a <see cref="User"/> object.
        /// </summary>
        /// <param name="dto">The DTO object containing user data.</param>
        /// <returns>A <see cref="User"/> object populated with the DTO values.</returns>
        public static User ToUser(this CreateUserDto dto)
        {
            return new User
            {
                Username = dto.Username,
                Email = dto.Email
            };
        }

        /// <summary>
        /// Updates an existing <see cref="User"/> object using the values from a <see cref="UpdateUserDto"/>.
        /// </summary>
        /// <param name="user">The <see cref="User"/> object to be updated.</param>
        /// <param name="dto">The <see cref="UpdateUserDto"/> containing updated user data.</param>
        public static void UpdateFromDto(this User user, UpdateUserDto dto)
        {
            Console.WriteLine($"User update: {JsonSerializer.Serialize(user)}");
            Console.WriteLine($"Data to update: {JsonSerializer.Serialize(dto)}");

            if (!string.IsNullOrEmpty(dto.UserName)) user.Username = dto.UserName;

            if (!string.IsNullOrEmpty(dto.Email)) user.Email = dto.Email;

            user.UpdatedAt = DateTime.UtcNow;
            Console.WriteLine($"User after update: {JsonSerializer.Serialize(user)}");
        }

        /// <summary>
        /// Converts a <see cref="Note"/> object to a <see cref="NoteDto"/>.
        /// </summary>
        /// <param name="note">The note object to be mapped.</param>
        /// <returns>A <see cref="NoteDto"/> object containing the note information.</returns>
        public static NoteDto ToNoteDto(this Note note)
        {
            return new NoteDto
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content,
                IsArchived = note.IsArchived,
                CreatedAt = note.CreatedAt,
                UpdatedAt = note.UpdatedAt,
                UserId = note.UserId
            };
        }

        /// <summary>
        /// Converts a <see cref="CreateNoteDto"/> to a <see cref="Note"/> object.
        /// </summary>
        /// <param name="dto">The DTO object containing note data.</param>
        /// <returns>A <see cref="Note"/> object populated with the DTO values.</returns>
        public static Note ToNote(this CreateNoteDto dto)
        {
            return new Note
            {
                Title = dto.Title,
                Content = dto.Content,
                UserId = dto.UserId,
                IsArchived = false
            };
        }

        /// <summary>
        /// Updates an existing <see cref="Note"/> object using the values from a <see cref="UpdateNoteDto"/>.
        /// </summary>
        /// <param name="note">The <see cref="Note"/> object to be updated.</param>
        /// <param name="dto">The <see cref="UpdateNoteDto"/> containing updated note data.</param>
        public static void UpdateFromDto(this Note note, UpdateNoteDto dto)
        {
            if (!string.IsNullOrEmpty(dto.Title)) note.Title = dto.Title;
            if (!string.IsNullOrEmpty(dto.Content)) note.Content = dto.Content;

            note.IsArchived = dto.IsArchived;
            note.UpdatedAt = DateTime.UtcNow;
        }
    }
}
