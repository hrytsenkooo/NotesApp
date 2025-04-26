using Microsoft.Extensions.DependencyInjection;
using NotesApp.Application.Services;

namespace NotesApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INoteService, NoteService>();

            return services;
        }
    }
}
