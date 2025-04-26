using NotesApp.Domain.Models;
using NotesApp.Infrastructure.Data.Models;

namespace NotesApp.Infrastructure.Data.Mappers
{
    public static class EntityMappers
    {
        public static UserItem ToUserItem(this User user)
        {
            return new UserItem
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public static User ToUser(this UserItem userItem)
        {
            return new User
            {
                Id = userItem.Id,
                UserName = userItem.UserName,
                Email = userItem.Email,
                CreatedAt = userItem.CreatedAt,
                UpdatedAt = userItem.UpdatedAt
            };
        }

        public static NoteItem ToNoteItem(this Note note)
        {
            return new NoteItem
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content,
                isArchived = note.IsArchived,
                CreatedAt = note.CreatedAt,
                UpdatedAt = note.UpdatedAt,
                UserId = note.UserId
            };
        }

        public static Note ToNote(this NoteItem noteItem)
        {
            return new Note
            {
                Id = noteItem.Id,
                Title = noteItem.Title,
                Content = noteItem.Content,
                IsArchived = noteItem.isArchived,
                CreatedAt = noteItem.CreatedAt,
                UpdatedAt = noteItem.UpdatedAt,
                UserId = noteItem.UserId
            };
        }
    }
}
