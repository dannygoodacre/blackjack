namespace Blackjack.Domain;

public interface IDomainEvent
{
    public Guid Id { get; init; }

    string StreamId { get; init; }

    DateTime Timestamp { get; init; }
}

public abstract record DomainEvent(string StreamId) : IDomainEvent
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string StreamId { get; init; } = StreamId;

    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
