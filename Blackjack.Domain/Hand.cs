namespace Blackjack.Domain;

public sealed record Hand
{
    public Guid Id { get; init; }

    public List<Card> Cards { get; init; } = [];
}
