using Blackjack.Requests;
using DannyGoodacre.Primitives;

namespace Blackjack.Grains;

[Alias("Blackjack.Grains.ITableGrain")]
public interface ITableGrain : IGrainWithGuidKey
{
    [Alias("CreateTAbleAsync")]
    Task<IResult> CreateTableAsync(CreateTableRequest request, Guid streamId, CancellationToken cancellationToken = default);

    [Alias("AddPlayerAsync")]
    Task<IResult> AddPlayerAsync(AddPlayerRequest request, CancellationToken cancellationToken = default);
}
