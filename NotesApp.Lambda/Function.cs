using Amazon.Lambda.Core;
using Amazon.Lambda.AppSyncEvents;
using NotesApp.Lambda.Functions;
using System.Text.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace NotesApp.Lambda
{
    public class Function
    {
        private readonly NoteFunctions _noteFunctions;
        private readonly UserFunctions _userFunctions;

        public Function()
        {
            _noteFunctions = new NoteFunctions();
            _userFunctions = new UserFunctions();
        }

        public async Task<object> FunctionHandler(AppSyncResolverEvent<Dictionary<string, object>> appsyncEvent, ILambdaContext context)
        {
            context.Logger.LogLine($"Received event: {JsonSerializer.Serialize(appsyncEvent)}");
            context.Logger.LogLine($"Resolver: {appsyncEvent.Info.FieldName}");

            return appsyncEvent.Info.FieldName switch
            {
                "getUser" => await _userFunctions.GetUserById(appsyncEvent, context),
                "listUsers" => await _userFunctions.GetAllUsers(appsyncEvent, context),
                "getNote" => await _noteFunctions.GetNoteById(appsyncEvent, context),
                "listNotes" => await _noteFunctions.GetAllNotes(appsyncEvent, context),
                "listNotesByUser" => await _noteFunctions.GetNotesByUserId(appsyncEvent, context),

                "createUser" => await _userFunctions.CreateUser(appsyncEvent, context),
                "updateUser" => await _userFunctions.UpdateUser(appsyncEvent, context),
                "deleteUser" => await _userFunctions.DeleteUser(appsyncEvent, context),
                "createNote" => await _noteFunctions.CreateNote(appsyncEvent, context),
                "updateNote" => await _noteFunctions.UpdateNote(appsyncEvent, context),
                "deleteNote" => await _noteFunctions.DeleteNote(appsyncEvent, context),

                _ => throw new Exception($"Unknown field: {appsyncEvent.Info.FieldName}")
            };
        }
    }
}