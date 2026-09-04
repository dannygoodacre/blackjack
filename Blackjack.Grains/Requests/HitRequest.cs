namespace Blackjack.Grains.Requests;

public sealed record HitRequest(Guid TableId, Guid PlayerId);
