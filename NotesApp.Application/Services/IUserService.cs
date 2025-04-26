using NotesApp.Application.DTOs;

namespace NotesApp.Application.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(string id);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
        Task<UserDto> UpdateUserAsync(string id, UpdateUserDto updateUserDto);
        Task DeleteUserAsync(string id);
    }
}
