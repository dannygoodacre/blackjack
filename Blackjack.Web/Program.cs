using Blackjack.Application;
using Blackjack.Grains;
using Blackjack.Requests;
using IResult = DannyGoodacre.Primitives.IResult;

namespace Blackjack.Web;

class Program
{
    public async static Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddLogging();

        builder.Services.AddApplication();

        builder.Services.AddEventPersistence(builder.Configuration.GetConnectionString("DefaultConnection")!);

        builder.Host.AddGrains();

        WebApplication app = builder.Build();

        await app.StartAsync();

        using IServiceScope scope = app.Services.CreateScope();

        IServiceProvider provider = scope.ServiceProvider;

        var factory = provider.GetRequiredService<IGrainFactory>();

        var grain = factory.GetGrain<ITableGrain>(Guid.NewGuid());

        IResult result = await grain.AddPlayerAsync(new AddPlayerRequest
        {
            StreamId = Guid.NewGuid(),
            PlayerId = Guid.NewGuid(),
            Name = "Player Name"
        }, CancellationToken.None);
    }
}
