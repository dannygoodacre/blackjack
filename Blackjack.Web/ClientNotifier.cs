using Blackjack.Grains;
using Microsoft.AspNetCore.SignalR;

namespace Blackjack.Web;

public sealed class ClientNotifier(IHubContext<BlackjackHub> hubContext) : IClientNotifier
{
    public Task NotifyPlayerJoinedAsync(Guid tableId, Guid playerId, string name)
        => hubContext.Clients
            .Group(tableId.ToString())
            .SendAsync("PlayerJoined", new { playerId, name });
}
