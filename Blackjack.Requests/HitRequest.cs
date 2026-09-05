namespace Blackjack.Requests;

public sealed record HitRequest : Request
{
    public Guid PlayerId { get; init; }

    public Guid HandId { get; init; }
}
