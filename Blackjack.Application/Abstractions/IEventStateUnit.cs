using Blackjack.Domain;
using DannyGoodacre.Cqrs;

namespace Blackjack.Application;

public interface IEventStateUnit : IStateUnit
{
    void Append(Guid streamId, IDomainEvent @event);

    void Append(Guid streamId, List<IDomainEvent> events);

    void Start(Guid streamId, IDomainEvent @event);

    void Start(Guid streamId, List<IDomainEvent> events);
}
