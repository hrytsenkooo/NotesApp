using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;
using NotesApp.Infrastructure.Data.Mappers;
using NotesApp.Infrastructure.Data.Models;
using System.Text.Json;

namespace NotesApp.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing <see cref="User"/> objects in DynamoDB.
    /// Provides CRUD operations for the <see cref="User"/> entity.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly IDynamoDBContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="dynamoDbClient">An instance of <see cref="IAmazonDynamoDB"/> to interact with DynamoDB.</param>
        public UserRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _context = new DynamoDBContext(dynamoDbClient);
        }

        /// <summary>
        /// Creates a new user in DynamoDB.
        /// </summary>
        /// <param name="user">The <see cref="User"/> object to be created.</param>
        /// <returns>The created <see cref="User"/> object with a new ID.</returns>
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

        /// <summary>
        /// Deletes a user by its ID from DynamoDB.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        public async Task DeleteAsync(string id)
        {
            await _context.DeleteAsync<UserItem>(id);
        }

        /// <summary>
        /// Retrieves all users from DynamoDB.
        /// </summary>
        /// <returns>A list of all <see cref="User"/> objects.</returns>
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

        /// <summary>
        /// Retrieves a user by their email address from DynamoDB.
        /// </summary>
        /// <param name="email">The email of the user to be retrieved.</param>
        /// <returns>The <see cref="User"/> object associated with the specified email, or null if not found.</returns>
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

        /// <summary>
        /// Retrieves a user by their ID from DynamoDB.
        /// </summary>
        /// <param name="id">The ID of the user to be retrieved.</param>
        /// <returns>The <see cref="User"/> object, or null if not found.</returns>
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

        /// <summary>
        /// Retrieves a user by their username from DynamoDB.
        /// </summary>
        /// <param name="username">The username of the user to be retrieved.</param>
        /// <returns>The <see cref="User"/> object associated with the specified username, or null if not found.</returns>
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

        /// <summary>
        /// Updates an existing user in DynamoDB.
        /// </summary>
        /// <param name="user">The <see cref="User"/> object to be updated.</param>
        /// <returns>The updated <see cref="User"/> object.</returns>
        public async Task<User> UpdateAsync(User user)
        {
            user.UpdatedAt = DateTime.UtcNow;

            var item = user.ToUserItem();
            await _context.SaveAsync(item);

            return user;
        }
    }
}
