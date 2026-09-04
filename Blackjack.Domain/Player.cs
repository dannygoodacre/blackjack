namespace Blackjack.Domain;

public sealed record Player
{
    public Guid Id { get; init; }

    public List<Card> Hand { get; init; } = [];
}
