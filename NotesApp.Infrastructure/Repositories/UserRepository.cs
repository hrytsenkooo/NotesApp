using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;
using NotesApp.Infrastructure.Data.Mappers;
using NotesApp.Infrastructure.Data.Models;
using System.Text.Json;

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
            var userItem = new UserItem
            {
                Id = Guid.NewGuid().ToString(),
                Username = user.Username,
                Email = user.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _context.SaveAsync(userItem);

            Console.WriteLine($"Saved user item: {JsonSerializer.Serialize(userItem)}");

            return userItem.ToUser();
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
            var config = new DynamoDBOperationConfig
            {
                IndexName = "EmailIndex"
            };

            var search = _context.QueryAsync<UserItem>(email, config);
            var items = await search.GetRemainingAsync();
            return items.FirstOrDefault()?.ToUser();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            Console.WriteLine($"Attempting to fetch user with ID: {id}");

            try
            {
                var userItem = await _context.LoadAsync<UserItem>(id);

                if (userItem == null)
                {
                    Console.WriteLine($"No user found with ID: {id}");
                    return null;
                }

                Console.WriteLine($"Found user: {JsonSerializer.Serialize(userItem)}");
                return userItem.ToUser();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var config = new DynamoDBOperationConfig
            {
                IndexName = "UsernameIndex"
            };

            var search = _context.QueryAsync<UserItem>(username, config);
            var items = await search.GetRemainingAsync();
            return items.FirstOrDefault()?.ToUser();
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
