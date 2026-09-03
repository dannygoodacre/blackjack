namespace Blackjack;

public interface ITableGrain : IGrainWithStringKey
{
    Task<int> HitAsync(string playerId, string card);
}
