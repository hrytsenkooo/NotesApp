using NotesApp.Application.DTOs;

namespace NotesApp.Application.Services
{
    public interface INoteService
    {
        Task<NoteDto> GetNoteByIdAsync(string id);
        Task<IEnumerable<NoteDto>> GetAllNotesAsync();
        Task<IEnumerable<NoteDto>> GetNotesByUserIdAsync(string userId);
        Task<NoteDto> CreateNoteAsync(CreateNoteDto createNoteDto);
        Task<NoteDto> UpdateNoteAsync(string id, UpdateNoteDto updateNoteDto);
        Task DeleteNoteAsync(string id);
    }
}
