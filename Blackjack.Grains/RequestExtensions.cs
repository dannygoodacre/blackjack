using Blackjack.Application.Commands;
using Blackjack.Requests;

namespace Blackjack.Grains;

internal static class RequestExtensions
{
    public static AddPlayerCommand ToCommand(this AddPlayerRequest request) => new(request.Id, request.Name);
}
