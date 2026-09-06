using Microsoft.Extensions.Hosting;

namespace Blackjack.Grains;

public static class ServiceCollectionExtensions
{
    extension(IHostBuilder hostBuilder)
    {
        public void AddGrains()
        {
            hostBuilder.UseOrleans(x =>
            {
                x.UseLocalhostClustering();

                // x.Services.AddSerializer(serializerBuilder =>
                // {
                //     serializerBuilder.AddJsonSerializer(
                //         isSupported: type => type.Namespace?.StartsWith("DannyGoodacre.Primitives") == true
                //     );
                // });
            });
        }
    }
}
