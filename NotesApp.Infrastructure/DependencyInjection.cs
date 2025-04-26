using Amazon.DynamoDBv2;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Domain.Interfaces;
using NotesApp.Infrastructure.Repositories;

namespace NotesApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IAmazonDynamoDB>(sp =>
            {
                var clientConfig = new AmazonDynamoDBConfig();
                return new AmazonDynamoDBClient(clientConfig);
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();

            return services;
        }
    }
}
