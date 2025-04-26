using NotesApp.Domain.Models;

namespace NotesApp.Domain.Interfaces
{
    public interface INoteRepository
    {
        Task<Note> GetByIdAsync(int id);
        Task<IEnumerable<Note>> GetAllAsync();
        Task<IEnumerable<Note>> GetByUserIdAsync(int userId);
        Task<Note> CreateAsync(Note note);
        Task<Note> UpdateAsync(Note note);
        Task DeleteAsync(int id);
    }
}
