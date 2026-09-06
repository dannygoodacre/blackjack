using Blackjack.Domain;

namespace Blackjack.Application;

public class TableContext : IAggregateContext
{
    public TableAggregate? Aggregate { get; set; }
}
