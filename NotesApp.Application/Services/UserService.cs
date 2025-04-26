using NotesApp.Application.DTOs;
using NotesApp.Application.Mappers;
using NotesApp.Domain.Interfaces;

namespace NotesApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            var existingUserByUsername = await _userRepository.GetByUsernameAsync(createUserDto.UserName);
            if (existingUserByUsername != null)
            {
                throw new Exception($"Username '{createUserDto.UserName}' is already taken");
            }

            var existingUserByEmail = await _userRepository.GetByEmailAsync(createUserDto.Email);
            if (existingUserByEmail != null)
            {
                throw new Exception($"Email '{createUserDto.Email}' is already registered");
            }

            var user = createUserDto.ToUser();
            var createdUser = await _userRepository.CreateAsync(user);

            return createdUser.ToUserDto();
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
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

            if (!string.IsNullOrEmpty(updateUserDto.UserName) && updateUserDto.UserName != user.UserName)
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

                user.UpdateFromDto(updateUserDto);
                var updateUser = await _userRepository.UpdateAsync(user);

                return updateUser.ToUserDto();
            }
        }
    }
}
