namespace Blackjack.Requests;

[GenerateSerializer]
[Alias("Blackjack.Requests.CreateTableRequest")]
public sealed record CreateTableRequest
{
    [Id(0)]
    public required string Name { get; init; }
}
