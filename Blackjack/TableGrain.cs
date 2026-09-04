namespace Blackjack;

public class TableGrain : Grain, ITableGrain
{
    private readonly TableAggregate _aggregate = new();

    private readonly

    public async Task<int> HitAsync(HitCommand command)
    {
        var result = _aggregate.Hit(command.PlayerId);

        if (result.IsFailure)
        {
            return;
        }

        var emittedEvents = result.Value;


    }
}

public record HitCommand
{
    public string PlayerId { get; init; }
}
