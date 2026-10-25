namespace Blackjack.Domain;

public interface IDomainEvent;

public sealed record TableCreatedEvent(string Name) : IDomainEvent;

public sealed record PlayerAddedEvent(Guid PlayerId, string Name) : IDomainEvent;
