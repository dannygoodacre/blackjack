using Blackjack.Grains;
using Blackjack.Grains.Requests;
using Microsoft.AspNetCore.SignalR;
using Orleans;

namespace Blackjack.Web;

public class BlackjackHub(IGrainFactory grainFactory) : Hub
{
    public async Task<IResult> HitAsync(HitRequest request)
    {
        var grain = grainFactory.GetGrain<ITableGrain>(request.TableId);

        await grain.HitAsync(request);
    }
}
