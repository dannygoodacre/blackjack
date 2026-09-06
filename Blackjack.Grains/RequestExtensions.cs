using Blackjack.Application.Commands;
using Blackjack.Requests;

namespace Blackjack.Grains;

internal static class RequestExtensions
{
    public static AddPlayerCommand ToCommand(this AddPlayerRequest request)
        => new(request.StreamId, request.PlayerId, request.Name);

    public static CreateTableCommand ToCommand(this CreateTableRequest request, Guid streamId)
        => new(streamId, request.Name);
}
