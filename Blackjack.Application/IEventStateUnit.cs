using Blackjack.Domain;
using DannyGoodacre.Cqrs;

namespace Blackjack.Application;

public interface IEventStateUnit : IStateUnit
{
    void Append(Guid streamId, List<IDomainEvent> events);
}
