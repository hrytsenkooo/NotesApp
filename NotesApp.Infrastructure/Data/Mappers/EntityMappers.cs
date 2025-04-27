using NotesApp.Domain.Models;
using NotesApp.Infrastructure.Data.Models;

namespace NotesApp.Infrastructure.Data.Mappers
{
    /// <summary>
    /// Provides methods for mapping between domain models and data models for DynamoDB.
    /// </summary>
    public static class EntityMappers
    {
        /// <summary>
        /// Converts a <see cref="User"/> domain model to a <see cref="UserItem"/> data model.
        /// </summary>
        /// <param name="user">The <see cref="User"/> domain model.</param>
        /// <returns>A <see cref="UserItem"/> data model.</returns>
        public static UserItem ToUserItem(this User user)
        {
            return new UserItem
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        /// <summary>
        /// Converts a <see cref="UserItem"/> data model to a <see cref="User"/> domain model.
        /// </summary>
        /// <param name="userItem">The <see cref="UserItem"/> data model.</param>
        /// <returns>A <see cref="User"/> domain model.</returns>
        public static User ToUser(this UserItem userItem)
        {
            return new User
            {
                Id = userItem.Id,
                Username = userItem.Username,
                Email = userItem.Email,
                CreatedAt = userItem.CreatedAt,
                UpdatedAt = userItem.UpdatedAt
            };
        }

        /// <summary>
        /// Converts a <see cref="Note"/> domain model to a <see cref="NoteItem"/> data model.
        /// </summary>
        /// <param name="note">The <see cref="Note"/> domain model.</param>
        /// <returns>A <see cref="NoteItem"/> data model.</returns>
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

        /// <summary>
        /// Converts a <see cref="NoteItem"/> data model to a <see cref="Note"/> domain model.
        /// </summary>
        /// <param name="noteItem">The <see cref="NoteItem"/> data model.</param>
        /// <returns>A <see cref="Note"/> domain model.</returns>
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
