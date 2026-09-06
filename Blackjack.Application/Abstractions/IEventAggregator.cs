namespace Blackjack.Application;

public interface IEventAggregator
{
    Task<TAggregate?> AggregateStreamAsync<TAggregate>(Guid streamId, CancellationToken cancellationToken = default)
        where TAggregate : class;
}
