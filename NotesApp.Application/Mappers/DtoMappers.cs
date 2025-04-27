using NotesApp.Application.DTOs;
using NotesApp.Domain.Models;
using System.Text.Json;

namespace NotesApp.Application.Mappers
{
    public static class DtoMappers
    { 
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

        public static User ToUser(this CreateUserDto dto)
        {
            return new User
            {
                Username = dto.Username,
                Email = dto.Email
            };
        }

        public static void UpdateFromDto(this User user, UpdateUserDto dto)
        {
            Console.WriteLine($"User update: {JsonSerializer.Serialize(user)}");
            Console.WriteLine($"Data to update: {JsonSerializer.Serialize(dto)}");

            if (!string.IsNullOrEmpty(dto.UserName)) user.Username = dto.UserName;

            if (!string.IsNullOrEmpty(dto.Email)) user.Email = dto.Email;

            user.UpdatedAt = DateTime.UtcNow;
            Console.WriteLine($"User after update: {JsonSerializer.Serialize(user)}");
        }

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

        public static void UpdateFromDto(this Note note, UpdateNoteDto dto)
        {
            if (!string.IsNullOrEmpty(dto.Title)) note.Title = dto.Title;
            if (!string.IsNullOrEmpty(dto.Content)) note.Content = dto.Content;

            note.IsArchived = dto.IsArchived;
            note.UpdatedAt = DateTime.UtcNow;
        }
    }
}
