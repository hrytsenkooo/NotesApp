using NotesApp.Application.DTOs;
using NotesApp.Domain.Models;

namespace NotesApp.Application.Mappers
{
    public static class DtoMappers
    {
        public static UserDto ToUserDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public static User ToUser(this CreateUserDto dto)
        {
            return new User
            {
                UserName = dto.UserName,
                Email = dto.Email
            };
        }

        public static void UpdateFromDto(this User user, UpdateUserDto dto)
        {
            if (!string.IsNullOrEmpty(dto.UserName)) user.UserName = dto.UserName;
            if (!string.IsNullOrEmpty(dto.Email)) user.Email = dto.Email;
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
            if (dto.Title != null) note.Title = dto.Title;
            if (dto.Content != null) note.Content = dto.Content;
            note.IsArchived = dto.IsArchived;
        }
    }
}
