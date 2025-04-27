using NotesApp.Application.DTOs;
using NotesApp.Application.Mappers;
using NotesApp.Domain.Interfaces;
using System.Text.Json;

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
            Console.WriteLine($"Creating note with UserId: {createNoteDto.UserId}");

            try
            {
                var user = await _userRepository.GetByIdAsync(createNoteDto.UserId);

                if (user == null)
                {
                    Console.WriteLine($"User with ID {createNoteDto.UserId} not found");
                    throw new Exception($"User with ID {createNoteDto.UserId} not found");
                }

                var note = createNoteDto.ToNote();
                note.Id = Guid.NewGuid().ToString();
                note.CreatedAt = DateTime.UtcNow;
                note.UpdatedAt = DateTime.UtcNow;

                var savedNote = await _noteRepository.CreateAsync(note);
                Console.WriteLine($"Note created successfully: {JsonSerializer.Serialize(savedNote)}");

                return savedNote.ToNoteDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateNoteAsync: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
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
