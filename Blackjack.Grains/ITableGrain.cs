using Blackjack.Requests;
using DannyGoodacre.Primitives;

namespace Blackjack.Grains;

public interface ITableGrain : IGrainWithGuidKey
{
    Task<IResult> AddPlayerAsync(AddPlayerRequest request, CancellationToken cancellationToken = default);
}
