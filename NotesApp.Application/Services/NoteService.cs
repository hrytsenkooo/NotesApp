using NotesApp.Application.DTOs;
using NotesApp.Application.Mappers;
using NotesApp.Domain.Interfaces;
using System.Text.Json;

namespace NotesApp.Application.Services
{
    /// <summary>
    /// Service class responsible for handling operations related to notes.
    /// </summary>
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteService"/> class.
        /// </summary>
        /// <param name="noteRepository">The repository to interact with notes data.</param>
        /// <param name="userRepository">The repository to interact with user data.</param>
        public NoteService(INoteRepository noteRepository, IUserRepository userRepository)
        {
            _noteRepository = noteRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Creates a new note asynchronously.
        /// </summary>
        /// <param name="createNoteDto">DTO containing the data to create the note.</param>
        /// <returns>A DTO of the created note.</returns>
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

        /// <summary>
        /// Deletes a note by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the note to be deleted.</param>
        /// <exception cref="Exception">Thrown if the note is not found.</exception>
        public async Task DeleteNoteAsync(string id)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            if (note == null)
            {
                throw new Exception($"Note with ID {id} not found");
            }

            await _noteRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Retrieves all notes asynchronously.
        /// </summary>
        /// <returns>A list of note DTOs.</returns>
        public async Task<IEnumerable<NoteDto>> GetAllNotesAsync()
        {
            var notes = await _noteRepository.GetAllAsync();
            return notes.Select(n => n.ToNoteDto());
        }

        /// <summary>
        /// Retrieves a note by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the note to retrieve.</param>
        /// <returns>The corresponding note DTO.</returns>
        /// <exception cref="Exception">Thrown if the note is not found.</exception>
        public async Task<NoteDto> GetNoteByIdAsync(string id)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            if (note == null)
            {
                throw new Exception($"Note with ID {id} not found");
            }

            return note.ToNoteDto();
        }

        /// <summary>
        /// Retrieves all notes associated with a specific user ID asynchronously.
        /// </summary>
        /// <param name="userId">The user ID to find notes for.</param>
        /// <returns>A list of note DTOs for the user.</returns>
        /// <exception cref="Exception">Thrown if the user is not found.</exception>
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

        /// <summary>
        /// Updates an existing note asynchronously.
        /// </summary>
        /// <param name="id">The ID of the note to update.</param>
        /// <param name="updateNoteDto">DTO containing the updated note information.</param>
        /// <returns>A DTO of the updated note.</returns>
        /// <exception cref="Exception">Thrown if the note is not found.</exception>
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
