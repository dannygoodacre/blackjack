namespace Blackjack.Requests;

public abstract record Request
{
    public Guid TableId { get; init; }
}
