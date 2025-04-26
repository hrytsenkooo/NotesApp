using NotesApp.Domain.Models;

namespace NotesApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task DeleteAsync(string id);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
    }
}
