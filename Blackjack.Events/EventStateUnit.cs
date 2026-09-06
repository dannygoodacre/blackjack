using Blackjack.Domain;
using Marten;

namespace Blackjack.Application;

public class EventStateUnit(IDocumentSession session) : IEventStateUnit
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await session.SaveChangesAsync(cancellationToken);

        return 0;
    }

    public void Append(Guid streamId, IDomainEvent @event)
        => session.Events.Append(streamId, @event);

    public void Append(Guid streamId, List<IDomainEvent> events)
        => session.Events.Append(streamId, events);

    public void Start(Guid streamId, IDomainEvent @event)
        => session.Events.StartStream(streamId, @event);

    public void Start(Guid streamId, List<IDomainEvent> events)
        => session.Events.StartStream(streamId, events);
}
