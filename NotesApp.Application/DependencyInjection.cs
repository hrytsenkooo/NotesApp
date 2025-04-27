using Microsoft.Extensions.DependencyInjection;
using NotesApp.Application.Services;

namespace NotesApp.Application
{
    /// <summary>
    /// A static class that registers application services in the dependency injection container.
    /// </summary>
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
