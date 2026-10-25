using Blackjack.Application;
using Marten;
using Microsoft.Extensions.DependencyInjection;

namespace Blackjack.Events;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddEventPersistence(string connectionString)
        {
            services.AddMarten(options =>
            {
                options.Connection(connectionString);
            });

            services.AddScoped<IEventStateUnit, EventStateUnit>();

            return services;
        }
    }
}
