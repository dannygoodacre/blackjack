namespace Blackjack.Requests;

public sealed record AddPlayerRequest : Request
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }
}
