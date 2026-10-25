using Blackjack.Application;
using Blackjack.Domain;
using DannyGoodacre.Primitives;

namespace Blackjack.Grains;

public sealed class TableGrain(IEventStateUnit stateUnit, IClientNotifier notifier) : Grain, ITableGrain
{
    private TableAggregate? _aggregate;

    public async override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        Guid tableId = this.GetPrimaryKey();

        _aggregate = await stateUnit.AggregateStreamAsync<TableAggregate>(tableId, cancellationToken);
    }

    public async Task<IResult> CreateTableAsync(string name, CancellationToken cancellationToken = default)
    {
        Guid tableId = this.GetPrimaryKey();

        if (_aggregate is not null)
        {
            return new Conflict($"A table with ID '{tableId}' already exists.");
        }

        var (aggregate, createdEvent) = TableAggregate.Create(tableId, name);

        stateUnit.Start(tableId, createdEvent);

        await stateUnit.SaveChangesAsync(cancellationToken);

        _aggregate = aggregate;

        return new Success();
    }

    public async Task<IResult> AddPlayerAsync(Guid playerId, string name, Guid streamId, CancellationToken cancellationToken = default)
    {
        Guid tableId = this.GetPrimaryKey();

        if (_aggregate is null)
        {
            return new DomainError($"Table '{tableId} does not exist");
        }

        var result = _aggregate.AddPlayer(playerId, name);

        if (result is not Success<List<IDomainEvent>>(var events))
        {
            return result;
        }

        stateUnit.Append(tableId, events);

        await stateUnit.SaveChangesAsync(cancellationToken);

        _ = notifier.NotifyPlayerJoinedAsync(tableId, playerId, name);

        return new Success();
    }
}
