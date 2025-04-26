using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Amazon.Lambda.AppSyncEvents;
using NotesApp.Application.DTOs;
using NotesApp.Application.Services;

namespace NotesApp.Lambda.Functions
{
    public class NoteFunctions
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INoteService _noteService;

        public NoteFunctions()
        {
            _serviceProvider = Startup.ConfigureServices();
            _noteService = _serviceProvider.GetRequiredService<INoteService>();
        }

        public async Task<object> GetNoteById(AppSyncResolverEvent<Dictionary<string, object>> evt, ILambdaContext context)
        {
            context.Logger.LogLine($"Getting note by ID: {evt.Arguments["id"]}");

            try
            {
                var noteId = evt.Arguments["id"].ToString();
                var note = await _noteService.GetNoteByIdAsync(noteId);
                return note;
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<object> GetAllNotes(AppSyncResolverEvent<Dictionary<string, object>> evt, ILambdaContext context)
        {
            context.Logger.LogLine("Getting all notes");

            try
            {
                var notes = await _noteService.GetAllNotesAsync();
                return notes;
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<object> GetNotesByUserId(AppSyncResolverEvent<Dictionary<string, object>> evt, ILambdaContext context)
        {
            var userId = evt.Arguments["userId"].ToString();
            context.Logger.LogLine($"Getting notes for user ID: {userId}");

            try
            {
                var notes = await _noteService.GetNotesByUserIdAsync(userId);
                return notes;
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<object> CreateNote(AppSyncResolverEvent<Dictionary<string, object>> evt, ILambdaContext context)
        {
            context.Logger.LogLine("Creating new note");

            try
            {
                var input = JObject.FromObject(evt.Arguments["input"]);
                var createNoteDto = new CreateNoteDto
                {
                    Title = input["title"].ToString(),
                    Content = input["content"].ToString(),
                    UserId = input["userId"].ToString()
                };

                var note = await _noteService.CreateNoteAsync(createNoteDto);
                return note;
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<object> UpdateNote(AppSyncResolverEvent<Dictionary<string, object>> evt, ILambdaContext context)
        {
            var noteId = evt.Arguments["id"].ToString();
            context.Logger.LogLine($"Updating note with ID: {noteId}");

            try
            {
                var input = JObject.FromObject(evt.Arguments["input"]);
                var updateNoteDto = new UpdateNoteDto();

                if (input["title"] != null)
                    updateNoteDto.Title = input["title"].ToString();

                if (input["content"] != null)
                    updateNoteDto.Content = input["content"].ToString();

                if (input["isArchived"] != null)
                    updateNoteDto.IsArchived = (bool)input["isArchived"];

                var note = await _noteService.UpdateNoteAsync(noteId, updateNoteDto);
                return note;
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<object> DeleteNote(AppSyncResolverEvent<Dictionary<string, object>> evt, ILambdaContext context)
        {
            var noteId = evt.Arguments["id"].ToString();
            context.Logger.LogLine($"Deleting note with ID: {noteId}");

            try
            {
                await _noteService.DeleteNoteAsync(noteId);
                return new { id = noteId, success = true };
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}