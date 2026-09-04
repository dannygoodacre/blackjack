using Blackjack.Domain;
using Blackjack.Grains.Requests;
using DannyGoodacre.Primitives;
using Orleans;

namespace Blackjack.Grains;

public sealed class TableGrain(IGrainFactory grainFactory) : Grain, ITableGrain
{
    private TableAggregate _aggregate = null!;

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        string tableId = this.GetPrimaryKeyString();

        _aggregate = new TableAggregate();

        return Task.CompletedTask;
    }

    public async Task<IResult> HitAsync(HitRequest request)
    {

    }
}
