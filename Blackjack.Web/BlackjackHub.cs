using Blackjack.Grains;
using Blackjack.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Orleans;

namespace Blackjack.Web;

public class BlackjackHub(IGrainFactory factory) : Hub
{
    public async Task<IActionResult> AddPlayerAsync(AddPlayerRequest request, CancellationToken cancellationToken = default)
    {
        var grain = factory.GetGrain<ITableGrain>(request.TableId);

        var result = await grain.AddPlayerAsync(request, cancellationToken);

        return result.ToHttpResponse();
    }
}
