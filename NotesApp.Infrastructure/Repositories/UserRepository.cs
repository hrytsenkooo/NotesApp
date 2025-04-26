using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;
using NotesApp.Infrastructure.Data.Mappers;
using NotesApp.Infrastructure.Data.Models;

namespace NotesApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDynamoDBContext _context;

        public UserRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _context = new DynamoDBContext(dynamoDbClient);
        }

        public async Task<User> CreateAsync(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            var item = user.ToUserItem();
            await _context.SaveAsync(item);

            return user;
        }

        public async Task DeleteAsync(string id)
        {
            await _context.DeleteAsync<UserItem>(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var scan = _context.ScanAsync<UserItem>(new List<ScanCondition>());
            var items = await scan.GetRemainingAsync();

            var users = new List<User>();
            foreach (var item in items)
            {
                users.Add(item.ToUser());
            }

            return users;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var query = _context.QueryAsync<UserItem>(email, new DynamoDBOperationConfig { IndexName = "EmailIndex" });

            var items = await query.GetRemainingAsync();
            var item = items.Count > 0 ? items[0] : null;

            return item?.ToUser();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var item = await _context.LoadAsync<UserItem>(id);
            return item?.ToUser();
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var query = _context.QueryAsync<UserItem>(username, new DynamoDBOperationConfig { IndexName = "UsernameIndex" });

            var items = await query.GetRemainingAsync();
            var item = items.Count > 0 ? items[0] : null;

            return item?.ToUser();
        }

        public async Task<User> UpdateAsync(User user)
        {
            user.UpdatedAt = DateTime.UtcNow;

            var item = user.ToUserItem();
            await _context.SaveAsync(item);

            return user;
        }
    }
}
