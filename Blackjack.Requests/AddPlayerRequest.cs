namespace Blackjack.Requests;

[GenerateSerializer]
[Alias("Blackjack.Requests.AddPlayerRequest")]
public sealed record AddPlayerRequest : Request
{
    [Id(0)]
    public required Guid Id { get; init; }

    [Id(1)]
    public required string Name { get; init; }
}
