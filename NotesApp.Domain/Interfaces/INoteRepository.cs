using NotesApp.Domain.Models;

namespace NotesApp.Domain.Interfaces
{
    public interface INoteRepository
    {
        Task<Note> GetByIdAsync(string id);
        Task<IEnumerable<Note>> GetAllAsync();
        Task<IEnumerable<Note>> GetByUserIdAsync(string userId);
        Task<Note> CreateAsync(Note note);
        Task<Note> UpdateAsync(Note note);
        Task DeleteAsync(string id);
        Task DeleteNotesByUserIdAsync(string userId);
    }
}
