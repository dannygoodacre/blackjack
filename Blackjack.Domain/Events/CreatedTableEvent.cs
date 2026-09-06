namespace Blackjack.Domain;

public sealed record CreatedTableEvent(string Name) : DomainEvent;
