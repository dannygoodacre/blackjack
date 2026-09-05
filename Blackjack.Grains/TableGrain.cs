using Blackjack.Application;
using Blackjack.Application.Commands;
using Blackjack.Domain;
using Blackjack.Requests;
using DannyGoodacre.Primitives;
using Microsoft.Extensions.DependencyInjection;
using Orleans;

namespace Blackjack.Grains;

public sealed class TableGrain(IServiceProvider serviceProvider) : Grain, ITableGrain
{
    private TableAggregate _aggregate = null!;

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        Guid tableId = this.GetPrimaryKey();

        // TODO: Fetch from Marten, etc.
        // This only runs once when the grain boots. Afterwards it uses the grain already in memory.
        _aggregate = new TableAggregate();

        return Task.CompletedTask;
    }

    public async Task<IResult> AddPlayerAsync(AddPlayerRequest request, CancellationToken cancellationToken = default)
    {
        await using var scope = serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<TableContext>();

        context.Aggregate = _aggregate;

        var hit = scope.ServiceProvider.GetRequiredService<IHit>();

        return hit.ExecuteAsync(request)
    }
}
