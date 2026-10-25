using Blackjack.Grains;
using Blackjack.Web.Requests;
using DannyGoodacre.Primitives;
using Microsoft.AspNetCore.SignalR;
using IResult = DannyGoodacre.Primitives.IResult;

namespace Blackjack.Web;

public sealed class BlackjackHub(IGrainFactory factory) : Hub
{
    public async Task<IResult> CreateTableAsync(string name, CancellationToken cancellationToken = default)
    {
        var state = new ValidationState();

        state.IsNotNullEmptyOrWhitespace(name, nameof(name));

        if (state.HasErrors)
        {
            return new Invalid(state);
        }

        var tableId = Guid.NewGuid();

        var grain = factory.GetGrain<ITableGrain>(tableId);

        var result = await grain.CreateTableAsync(name, cancellationToken);

        return result;
    }

    public async Task<IResult> AddPlayerAsync(AddPlayerRequest request, CancellationToken cancellationToken = default)
    {
        var state = new ValidationState();

        state.IsNonEmptyGuid(request.TableId, nameof(request.TableId));

        state.IsNonEmptyGuid(request.PlayerId, nameof(request.PlayerId));

        state.IsNotNullEmptyOrWhitespace(request.Name, nameof(request.Name));

        if (state.HasErrors)
        {
            return new Invalid(state);
        }

        var grain = factory.GetGrain<ITableGrain>(request.TableId);

        return await grain.AddPlayerAsync(request.PlayerId, request.Name, request.TableId, cancellationToken);
    }
}
