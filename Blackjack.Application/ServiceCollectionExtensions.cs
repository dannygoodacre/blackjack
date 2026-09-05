using Microsoft.Extensions.DependencyInjection;

namespace Blackjack.Application;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplication()
        {
            services.AddCommandHandlers(typeof(ServiceCollectionExtensions).Assembly);

            services.AddQueryHandlers(typeof(ServiceCollectionExtensions).Assembly);

            services.AddScoped<TableContext>();

            return services;
        }
    }
}
