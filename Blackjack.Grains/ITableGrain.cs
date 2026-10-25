using DannyGoodacre.Primitives;

namespace Blackjack.Grains;

[Alias("Blackjack.Grains.ITableGrain")]
public interface ITableGrain : IGrainWithGuidKey
{
    [Alias("CreateTableAsync")]
    Task<IResult> CreateTableAsync(string name, CancellationToken cancellationToken = default);

    [Alias("AddPlayerAsync")]
    Task<IResult> AddPlayerAsync(Guid playerId, string name, Guid streamId, CancellationToken cancellationToken = default);
}
