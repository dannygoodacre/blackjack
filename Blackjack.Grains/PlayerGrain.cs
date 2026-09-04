using Blackjack.Domain;
using Blackjack.Grains.Requests;
using Orleans;

namespace Blackjack.Grains;

public sealed class PlayerGrain : Grain, IPlayerGrain
{
    private PlayerAggregate _aggregate = null!;

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        string tableId = this.GetPrimaryKeyString();

        _aggregate = new PlayerAggregate();

        return Task.CompletedTask;
    }
}

public interface IPlayerGrain : IGrainWithGuidKey
{
}
