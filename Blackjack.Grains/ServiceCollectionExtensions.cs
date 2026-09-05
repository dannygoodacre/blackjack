using Microsoft.Extensions.Hosting;

namespace Blackjack.Grains;

public static class ServiceCollectionExtensions
{
    extension(IHostBuilder hostBuilder)
    {
        public void AddGrains() => hostBuilder.UseOrleans(x => x.UseLocalhostClustering());
    }
}
