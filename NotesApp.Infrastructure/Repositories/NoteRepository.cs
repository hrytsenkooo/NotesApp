using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;
using NotesApp.Infrastructure.Data.Mappers;
using NotesApp.Infrastructure.Data.Models;

namespace NotesApp.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="Note"/> objects in DynamoDB.
    /// Provides CRUD operations for the <see cref="Note"/> entity.
    /// </summary>
    public class NoteRepository : INoteRepository
    {
        private readonly IDynamoDBContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteRepository"/> class.
        /// </summary>
        /// <param name="dynamoDbClient">An instance of <see cref="IAmazonDynamoDB"/> to interact with DynamoDB.</param>
        public NoteRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _context = new DynamoDBContext(dynamoDbClient);
        }

        /// <summary>
        /// Creates a new note in DynamoDB.
        /// </summary>
        /// <param name="note">The <see cref="Note"/> object to be created.</param>
        /// <returns>The created <see cref="Note"/> object with a new ID.</returns>
        public async Task<Note> CreateAsync(Note note)
        {
            note.Id = Guid.NewGuid().ToString();
            note.CreatedAt = DateTime.UtcNow;
            note.UpdatedAt = DateTime.UtcNow;

            var item = note.ToNoteItem();
            await _context.SaveAsync(item);

            return note;
        }

        /// <summary>
        /// Deletes a note by its ID from DynamoDB.
        /// </summary>
        /// <param name="id">The ID of the note to be deleted.</param>
        public async Task DeleteAsync(string id)
        {
            await _context.DeleteAsync<NoteItem>(id);
        }

        /// <summary>
        /// Retrieves all notes from DynamoDB.
        /// </summary>
        /// <returns>A list of all <see cref="Note"/> objects.</returns>
        public async Task<IEnumerable<Note>> GetAllAsync()
        {
            var scan = _context.ScanAsync<NoteItem>(new List<ScanCondition>());
            var items = await scan.GetRemainingAsync();

            var notes = new List<Note>();
            foreach (var item in items)
            {
                notes.Add(item.ToNote());
            }

            return notes;
        }

        /// <summary>
        /// Retrieves a note by its ID from DynamoDB.
        /// </summary>
        /// <param name="id">The ID of the note to be retrieved.</param>
        /// <returns>The <see cref="Note"/> object, or null if not found.</returns>
        public async Task<Note> GetByIdAsync(string id)
        {
            var item = await _context.LoadAsync<NoteItem>(id);
            return item?.ToNote();
        }

        /// <summary>
        /// Retrieves notes by the user ID from DynamoDB.
        /// </summary>
        /// <param name="userId">The ID of the user whose notes are to be retrieved.</param>
        /// <returns>A list of <see cref="Note"/> objects belonging to the specified user.</returns>
        public async Task<IEnumerable<Note>> GetByUserIdAsync(string userId)
        {
            var query = _context.QueryAsync<NoteItem>(userId, new DynamoDBOperationConfig { IndexName = "UserIdIndex" });

            var items = await query.GetRemainingAsync();

            var notes = new List<Note>();
            foreach (var item in items)
            {
                notes.Add(item.ToNote());
            }

            return notes;
        }

        /// <summary>
        /// Updates an existing note in DynamoDB.
        /// </summary>
        /// <param name="note">The <see cref="Note"/> object to be updated.</param>
        /// <returns>The updated <see cref="Note"/> object.</returns>
        public async Task<Note> UpdateAsync(Note note)
        {
            note.UpdatedAt = DateTime.UtcNow;

            var item = note.ToNoteItem();
            await _context.SaveAsync(item);

            return note;
        }

        /// <summary>
        /// Deletes all notes for a specific user by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose notes are to be deleted.</param>
        public async Task DeleteNotesByUserIdAsync(string userId)
        {
            Console.WriteLine($"Deleting all notes for user: {userId}");

            var notes = await _context.QueryAsync<NoteItem>(
                userId,
                new DynamoDBOperationConfig
                {
                    IndexName = "UserIdIndex"
                }).GetRemainingAsync();

            Console.WriteLine($"Found {notes.Count} notes to delete");

            foreach (var note in notes)
            {
                Console.WriteLine($"Deleting note: {note.Id}");
                await _context.DeleteAsync<NoteItem>(note.Id);
            }
        }
    }
}
