using Blackjack.Domain;

namespace Blackjack.Application;

public interface IAggregateContext
{
    TableAggregate? Aggregate { get; set; }
}
