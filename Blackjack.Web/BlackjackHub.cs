using Blackjack.Grains;
using Blackjack.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Blackjack.Web;

public class BlackjackHub(IGrainFactory factory) : Hub
{
    public async Task<IActionResult> CreateTableAsync(CreateTableRequest request, CancellationToken cancellationToken)
    {
        var streamId = Guid.NewGuid();

        var grain = factory.GetGrain<ITableGrain>(streamId);

        var result = await grain.CreateTableAsync(request, streamId, cancellationToken);

        return result.ToHttpResponse();
    }

    public async Task<IActionResult> AddPlayerAsync(AddPlayerRequest request, CancellationToken cancellationToken = default)
    {
        var grain = factory.GetGrain<ITableGrain>(request.StreamId);

        var result = await grain.AddPlayerAsync(request, cancellationToken);

        return result.ToHttpResponse();
    }
}
