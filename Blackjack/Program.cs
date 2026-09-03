namespace Blackjack;

class Program
{
    async static Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
            .UseOrleans(siloBuilder =>
            {
                // For local dev/test.
                siloBuilder.UseLocalhostClustering();

                // Configure in-memory storage for Grain state persistence
                siloBuilder.AddMemoryGrainStorage("TableStateStore");
            });

        using var host = builder.Build();

        await host.StartAsync();

        var grainFactory = host.Services.GetRequiredService<IGrainFactory>();

        // Orleans will automatically activate the grain in memory on demand
        var tableGrain = grainFactory.GetGrain<ITableGrain>("table-101");

        // 4. Interact with the Grain asynchronously
        _ = await tableGrain.HitAsync("player-1", "10_Spades");

        var currentTotal = await tableGrain.HitAsync("player-2", "10_Spades");

        Console.WriteLine($"Player hit! Current hand total: {currentTotal}");

        await host.StopAsync();
    }
}
