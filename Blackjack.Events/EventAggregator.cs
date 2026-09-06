using Marten;

namespace Blackjack.Application;

public sealed class EventAggregator(IDocumentSession session) : IEventAggregator
{
    public Task<TAggregate?> AggregateStreamAsync<TAggregate>(Guid streamId, CancellationToken cancellationToken = default)
        where TAggregate : class
        => session.Events.AggregateStreamAsync<TAggregate>(streamId, token: cancellationToken);
}
