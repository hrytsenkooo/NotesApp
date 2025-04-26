using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Application.Services;
using Amazon.Lambda.AppSyncEvents;
using NotesApp.Application.DTOs;
using Newtonsoft.Json.Linq;

namespace NotesApp.Lambda.Functions
{
    public class UserFunctions
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserService _userService;

        public UserFunctions()
        {
            _serviceProvider = Startup.ConfigureServices();
            _userService = _serviceProvider.GetRequiredService<IUserService>(); 
        }

        public async Task<object> GetUserById(AppSyncResolverEvent<Dictionary<string, object>> evt, ILambdaContext context)
        {
            context.Logger.LogLine($"Getting user by ID: {evt.Arguments["id"]}");

            try
            {
                var userId = evt.Arguments["id"].ToString();
                var user = await _userService.GetUserByIdAsync(userId);
                return user;
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<object> GetAllUsers(AppSyncResolverEvent<Dictionary<string, object>> evt, ILambdaContext context)
        {
            context.Logger.LogLine("Getting all users");

            try
            {
                var users = await _userService.GetAllUsersAsync();
                return users;
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<object> CreateUser(AppSyncResolverEvent<Dictionary<string, object>> evt, ILambdaContext context)
        {
            context.Logger.LogLine("Creating new user");

            try
            {
                var input = JObject.FromObject(evt.Arguments["input"]);
                var createUserDto = new CreateUserDto
                {
                    UserName = input["username"].ToString(),
                    Email = input["email"].ToString()
                };

                var user = await _userService.CreateUserAsync(createUserDto);
                return user;
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<object> UpdateUser(AppSyncResolverEvent<Dictionary<string, object>> evt, ILambdaContext context)
        {
            var userId = evt.Arguments["id"].ToString();
            context.Logger.LogLine($"Updating user with ID: {userId}");

            try
            {
                var input = JObject.FromObject(evt.Arguments["input"]);
                var updateUserDto = new UpdateUserDto();

                if (input["username"] != null)
                    updateUserDto.UserName = input["username"].ToString();

                if (input["email"] != null)
                    updateUserDto.Email = input["email"].ToString();

                var user = await _userService.UpdateUserAsync(userId, updateUserDto);
                return user;
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<object> DeleteUser(AppSyncResolverEvent<Dictionary<string, object>> evt, ILambdaContext context)
        {
            var userId = evt.Arguments["id"].ToString();
            context.Logger.LogLine($"Deleting user with ID: {userId}");

            try
            {
                await _userService.DeleteUserAsync(userId);
                return new { id = userId, success = true };
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}