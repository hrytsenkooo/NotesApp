using Microsoft.Extensions.DependencyInjection;
using NotesApp.Application;
using NotesApp.Infrastructure;

namespace NotesApp.Lambda
{
    public class Startup
    {
        public static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddApplication();
            services.AddInfrastructure();

            return services.BuildServiceProvider();
        }
    }
}
