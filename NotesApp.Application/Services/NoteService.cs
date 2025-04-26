using NotesApp.Application.DTOs;
using NotesApp.Application.Mappers;
using NotesApp.Domain.Interfaces;

namespace NotesApp.Application.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IUserRepository _userRepository;

        public NoteService(INoteRepository noteRepository, IUserRepository userRepository)
        {
            _noteRepository = noteRepository;
            _userRepository = userRepository;
        }

        public async Task<NoteDto> CreateNoteAsync(CreateNoteDto createNoteDto)
        {
            var user = await _userRepository.GetByIdAsync(createNoteDto.UserId);
            if (user == null)
            {
                throw new Exception($"User with ID {createNoteDto.UserId} not found");
            }

            var note = createNoteDto.ToNote();
            var createdNote = await _noteRepository.CreateAsync(note);

            return createdNote.ToNoteDto();
        }

        public async Task DeleteNoteAsync(string id)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            if (note == null)
            {
                throw new Exception($"Note with ID {id} not found");
            }

            await _noteRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<NoteDto>> GetAllNotesAsync()
        {
            var notes = await _noteRepository.GetAllAsync();
            return notes.Select(n => n.ToNoteDto());
        }

        public async Task<NoteDto> GetNoteByIdAsync(string id)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            if (note == null)
            {
                throw new Exception($"Note with ID {id} not found");
            }

            return note.ToNoteDto();
        }

        public async Task<IEnumerable<NoteDto>> GetNotesByUserIdAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception($"User with ID {userId} not found");
            }

            var notes = await _noteRepository.GetByUserIdAsync(userId);
            return notes.Select(n => n.ToNoteDto());
        }

        public async Task<NoteDto> UpdateNoteAsync(string id, UpdateNoteDto updateNoteDto)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            if (note == null)
            {
                throw new Exception($"Note with ID {id} not found");
            }

            note.UpdateFromDto(updateNoteDto);
            var updatedNote = await _noteRepository.UpdateAsync(note);

            return updatedNote.ToNoteDto();
        }
    }
}
