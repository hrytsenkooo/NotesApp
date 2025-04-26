using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;
using NotesApp.Infrastructure.Data.Mappers;
using NotesApp.Infrastructure.Data.Models;

namespace NotesApp.Infrastructure.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly IDynamoDBContext _context;

        public NoteRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _context = new DynamoDBContext(dynamoDbClient);
        }

        public async Task<Note> CreateAsync(Note note)
        {
            note.Id = Guid.NewGuid().ToString();
            note.CreatedAt = DateTime.UtcNow;
            note.UpdatedAt = DateTime.UtcNow;

            var item = note.ToNoteItem();
            await _context.SaveAsync(item);

            return note;
        }

        public async Task DeleteAsync(string id)
        {
            await _context.DeleteAsync<NoteItem>(id);
        }

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

        public async Task<Note> GetByIdAsync(string id)
        {
            var item = await _context.LoadAsync<NoteItem>(id);
            return item?.ToNote();
        }

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

        public async Task<Note> UpdateAsync(Note note)
        {
            note.UpdatedAt = DateTime.UtcNow;

            var item = note.ToNoteItem();
            await _context.SaveAsync(item);

            return note;
        }
    }
}
