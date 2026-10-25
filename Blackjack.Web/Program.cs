using Blackjack.Events;
using Blackjack.Grains;
using IResult = DannyGoodacre.Primitives.IResult;

namespace Blackjack.Web;

class Program
{
    public async static Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddLogging();

        builder.Services.AddEventPersistence(builder.Configuration.GetConnectionString("DefaultConnection")!);

        builder.Services.AddSignalR();

        builder.Services.AddSingleton<IClientNotifier, ClientNotifier>();

        builder.Host.AddGrains();

        WebApplication app = builder.Build();

        await app.StartAsync();

        using IServiceScope scope = app.Services.CreateScope();

        IServiceProvider provider = scope.ServiceProvider;

        var factory = provider.GetRequiredService<IGrainFactory>();

        var grain = factory.GetGrain<ITableGrain>(Guid.NewGuid());

        await grain.CreateTableAsync("Table Name");

        await grain.AddPlayerAsync(Guid.NewGuid(), "Player Name", Guid.NewGuid());
    }
}
