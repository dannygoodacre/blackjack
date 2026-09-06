namespace Blackjack.Requests;

[GenerateSerializer]
[Alias("Blackjack.Requests.AddPlayerRequest")]
public sealed record AddPlayerRequest : Request
{
    [Id(0)]
    public required Guid PlayerId { get; init; }

    [Id(1)]
    public required string Name { get; init; }
}
