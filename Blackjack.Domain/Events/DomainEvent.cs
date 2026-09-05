namespace Blackjack.Domain;

public interface IDomainEvent
{
    public Guid Id { get; init; }

    DateTime Timestamp { get; init; }
}

public abstract record DomainEvent : IDomainEvent
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}

public sealed record PlayerAdded(Guid PlayerId, string Name) : DomainEvent;
