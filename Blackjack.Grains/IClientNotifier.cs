namespace Blackjack.Grains;

public interface IClientNotifier
{
    Task NotifyPlayerJoinedAsync(Guid tableId, Guid playerId, string name);
}
