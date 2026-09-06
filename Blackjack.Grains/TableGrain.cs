using Blackjack.Application;
using Blackjack.Application.Commands;
using Blackjack.Domain;
using Blackjack.Requests;
using DannyGoodacre.Primitives;
using Microsoft.Extensions.DependencyInjection;

namespace Blackjack.Grains;

public sealed class TableGrain(IServiceProvider serviceProvider, IEventAggregator eventAggregator) : Grain, ITableGrain
{
    private TableAggregate? _aggregate;

    public async override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        Guid tableId = this.GetPrimaryKey();

        _aggregate = await eventAggregator.AggregateStreamAsync<TableAggregate>(tableId, cancellationToken);
    }

    public async Task<IResult> CreateTableAsync(CreateTableRequest request, Guid streamId, CancellationToken cancellationToken = default)
    {
        await using var scope = serviceProvider.CreateAsyncScope();

        var createTable = scope.ServiceProvider.GetRequiredService<ICreateTable>();

        return await createTable.ExecuteAsync(request.ToCommand(streamId), cancellationToken);
    }

    public async Task<IResult> AddPlayerAsync(AddPlayerRequest request, CancellationToken cancellationToken = default)
    {
        await using var scope = serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<IAggregateContext>();

        context.Aggregate = _aggregate;

        var hit = scope.ServiceProvider.GetRequiredService<IAddPlayer>();

        return await hit.ExecuteAsync(request.ToCommand(), cancellationToken);
    }
}
