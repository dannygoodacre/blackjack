namespace Blackjack.Web.Requests;

public sealed record AddPlayerRequest(Guid PlayerId, string Name, Guid TableId);
