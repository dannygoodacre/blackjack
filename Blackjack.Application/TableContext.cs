using Blackjack.Domain;

namespace Blackjack.Application;

public class TableContext
{
    public TableAggregate Aggregate { get; set; } = null!;
}
