using System.Text.Json;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Application;
using NotesApp.Infrastructure;
using NotesApp.Application.DTOs;
using NotesApp.Application.Services;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace NotesApp.Lambda
{
    /// <summary>
    /// The main entry point for the AWS Lambda function that handles AppSync GraphQL requests.
    /// </summary>
    public class Function
    {
        private readonly ServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes the Lambda function by setting up dependency injection and serializer options.
        /// </summary>
        public Function()
        {
            var services = new ServiceCollection();
            services.AddApplication();
            services.AddInfrastructure();
            _serviceProvider = services.BuildServiceProvider();
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        /// <summary>
        /// The handler for incoming AppSync events (GraphQL queries and mutations).
        /// </summary>
        /// <param name="appsyncEvent">The event data received from AppSync.</param>
        /// <param name="context">The Lambda context providing details about the invocation.</param>
        /// <returns>The result of the GraphQL query or mutation.</returns>
        public async Task<object> FunctionHandler(AppSyncEvent appsyncEvent, ILambdaContext context)
        {
            context.Logger.LogLine($"Received event: {JsonSerializer.Serialize(appsyncEvent)}");

            var operationType = appsyncEvent.Info.ParentTypeName;
            var fieldName = appsyncEvent.Info.FieldName;

            switch (operationType)
            {
                case "Query":
                    return await HandleQuery(fieldName, appsyncEvent.Arguments, context);
                case "Mutation":
                    return await HandleMutation(fieldName, appsyncEvent.Arguments, context);
                default:
                    throw new LambdaException($"Unsupported operation type: {operationType}");
            }
        }

        /// <summary>
        /// Handles GraphQL queries based on the field name.
        /// </summary>
        /// <param name="fieldName">The field name of the GraphQL query.</param>
        /// <param name="arguments">The arguments passed with the query.</param>
        /// <param name="context">The Lambda context for logging and tracing.</param>
        /// <returns>The result of the query.</returns>
        private async Task<object> HandleQuery(string fieldName, Dictionary<string, object> arguments, ILambdaContext context)
        {
            switch (fieldName)
            {
                case "getUserById":
                    {
                        var userService = _serviceProvider.GetRequiredService<IUserService>();
                        var id = arguments["id"].ToString();
                        var user = await userService.GetUserByIdAsync(id);
                        return user;
                    }
                case "getAllUsers":
                    {
                        var userService = _serviceProvider.GetRequiredService<IUserService>();
                        var users = await userService.GetAllUsersAsync();
                        return users;
                    }
                case "getNoteById":
                    {
                        var noteService = _serviceProvider.GetRequiredService<INoteService>();
                        var id = arguments["id"].ToString();
                        var note = await noteService.GetNoteByIdAsync(id);
                        return note;
                    }
                case "getAllNotes":
                    {
                        var noteService = _serviceProvider.GetRequiredService<INoteService>();
                        var notes = await noteService.GetAllNotesAsync();
                        return notes;
                    }
                case "getNotesByUserId":
                    {
                        var noteService = _serviceProvider.GetRequiredService<INoteService>();
                        var userId = arguments["userId"].ToString();
                        var notes = await noteService.GetNotesByUserIdAsync(userId);
                        return notes;
                    }
                default:
                    throw new LambdaException($"Unknown field name: {fieldName}");
            }
        }

        /// <summary>
        /// Handles GraphQL mutations based on the field name.
        /// </summary>
        /// <param name="fieldName">The field name of the GraphQL mutation.</param>
        /// <param name="arguments">The arguments passed with the mutation.</param>
        /// <param name="context">The Lambda context for logging and tracing.</param>
        /// <returns>The result of the mutation.</returns>
        private async Task<object> HandleMutation(string fieldName, Dictionary<string, object> arguments, ILambdaContext context)
        {
            switch (fieldName)
            {
                case "createUser":
                    {
                        var userService = _serviceProvider.GetRequiredService<IUserService>();

                        context.Logger.LogLine($"Input arguments: {JsonSerializer.Serialize(arguments["input"])}");

                        var inputJson = JsonSerializer.Serialize(arguments["input"]);
                        var input = JsonSerializer.Deserialize<CreateUserDto>(inputJson);

                        context.Logger.LogLine($"Deserialized input: {JsonSerializer.Serialize(input)}");

                        if (input == null) throw new ArgumentException("Invalid input data");

                        var createdUser = await userService.CreateUserAsync(input);

                        if (createdUser == null) throw new Exception("User creation failed");

                        return createdUser;
                    }
                case "updateUser":
                    {
                        var userService = _serviceProvider.GetRequiredService<IUserService>();
                        var id = arguments["id"].ToString();
                        var inputJson = JsonSerializer.Serialize(arguments["input"]);
                        var input = JsonSerializer.Deserialize<UpdateUserDto>(inputJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        var user = await userService.UpdateUserAsync(id, input);
                        return user;
                    }
                case "deleteUser":
                    {
                        var userService = _serviceProvider.GetRequiredService<IUserService>();
                        var id = arguments["id"].ToString();
                        await userService.DeleteUserAsync(id);
                        return new { id };
                    }
                case "createNote":
                    {
                        var noteService = _serviceProvider.GetRequiredService<INoteService>();
                        var inputJson = JsonSerializer.Serialize(arguments["input"]);
                        var input = JsonSerializer.Deserialize<CreateNoteDto>(inputJson);

                        context.Logger.LogLine($"CreateNote input: {inputJson}");

                        if (input == null) throw new ArgumentException("Invalid note input data");

                        var note = await noteService.CreateNoteAsync(input);
                        return note;
                    }
                case "updateNote":
                    {
                        var noteService = _serviceProvider.GetRequiredService<INoteService>();
                        var id = arguments["id"].ToString();
                        var inputJson = JsonSerializer.Serialize(arguments["input"]);
                        var input = JsonSerializer.Deserialize<UpdateNoteDto>(inputJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        var note = await noteService.UpdateNoteAsync(id, input);
                        return note;
                    }
                case "deleteNote":
                    {
                        var noteService = _serviceProvider.GetRequiredService<INoteService>();
                        var id = arguments["id"].ToString();
                        await noteService.DeleteNoteAsync(id);
                        return new { id };
                    }
                default:
                    throw new LambdaException($"Unknown field name: {fieldName}");
            }
        }
    }

    /// <summary>
    /// Custom exception for handling Lambda-specific errors.
    /// </summary>
    public class LambdaException : Exception
    {
        public LambdaException(string message) : base(message) { }
    }

    /// <summary>
    /// Represents the event received from AppSync containing information about the request.
    /// </summary>
    public class AppSyncEvent
    {
        public AppSyncInfo Info { get; set; }
        public Dictionary<string, object> Arguments { get; set; }
    }

    /// <summary>
    /// Contains information about the GraphQL query or mutation, including the field name and parent type name.
    /// </summary>
    public class AppSyncInfo
    {
        public string FieldName { get; set; }
        public string ParentTypeName { get; set; }
    }
}