namespace Blackjack.Requests;

[GenerateSerializer]
[Alias("Blackjack.Requests.Request")]
public abstract record Request
{
    [Id(0)]
    public Guid TableId { get; init; }
}
