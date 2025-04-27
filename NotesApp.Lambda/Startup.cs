using Microsoft.Extensions.DependencyInjection;
using NotesApp.Application;
using NotesApp.Infrastructure;

namespace NotesApp.Lambda
{
    /// <summary>
    /// The Startup class configures the services required for the Lambda function.
    /// It sets up dependency injection for application and infrastructure services.
    /// </summary>
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
