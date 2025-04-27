using NotesApp.Application.DTOs;
using NotesApp.Application.Mappers;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;
using System.Text.Json;

namespace NotesApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly INoteRepository _noteRepository;

        public UserService(IUserRepository userRepository, INoteRepository noteRepository)
        {
            _userRepository = userRepository;
            _noteRepository = noteRepository;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            if (string.IsNullOrWhiteSpace(createUserDto.Username)) throw new ArgumentException("Username cannot be empty");
            if (string.IsNullOrWhiteSpace(createUserDto.Email)) throw new ArgumentException("Email cannot be empty");

            var username = createUserDto.Username.Trim().ToLower();
            var email = createUserDto.Email.Trim().ToLower();

            var existingUserByUsername = await _userRepository.GetByUsernameAsync(username);
            if (existingUserByUsername != null) throw new Exception($"Username '{username}' is already taken");

            var existingUserByEmail = await _userRepository.GetByEmailAsync(email);
            if (existingUserByEmail != null) throw new Exception($"Email '{email}' is already registered");

            var user = new User
            {
                Username = username,
                Email = email
            };

            var createdUser = await _userRepository.CreateAsync(user);

            Console.WriteLine($"Created user DTO: {JsonSerializer.Serialize(createdUser.ToUserDto())}");

            var userDto = createdUser?.ToUserDto();
            if (userDto == null)
                throw new Exception("Failed to create user");

            return userDto;
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            await _noteRepository.DeleteNotesByUserIdAsync(id);
            await _userRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => u.ToUserDto());
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception($"User with ID {id} not found");
            }
            return user.ToUserDto();
        }

        public async Task<UserDto> UpdateUserAsync(string id, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception($"User with ID {id} not found");
            }

            if (!string.IsNullOrEmpty(updateUserDto.UserName) && updateUserDto.UserName != user.Username)
            {
                var existingUserByUsername = await _userRepository.GetByUsernameAsync(updateUserDto.UserName);
                if (existingUserByUsername != null)
                {
                    throw new Exception($"Email '{updateUserDto.Email} is already registered");
                }
            }

            if (!string.IsNullOrEmpty(updateUserDto.Email) && updateUserDto.Email != user.Email)
            {
                var existingUserByEmail = await _userRepository.GetByEmailAsync(updateUserDto.Email);
                if (existingUserByEmail != null)
                {
                    throw new Exception($"Email '{updateUserDto.Email}' is already registered");
                }
            }

            user.UpdateFromDto(updateUserDto);
            user.UpdatedAt = DateTime.UtcNow;
            var updateUser = await _userRepository.UpdateAsync(user);

            return updateUser.ToUserDto();
        }
    }
}
