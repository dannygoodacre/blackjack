using Blackjack.Grains.Requests;
using DannyGoodacre.Primitives;
using Orleans;

namespace Blackjack.Grains;

public interface ITableGrain : IGrainWithGuidKey
{
    Task<IResult> HitAsync(HitRequest request);
}
