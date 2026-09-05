namespace Blackjack.Domain;

public sealed record Player(Guid Id, string Name)
{
    public List<Hand> Hands { get; init; } = [];
}
