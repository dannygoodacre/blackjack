using System.Runtime.InteropServices;
using DannyGoodacre.Primitives;

namespace Blackjack.Domain;

public sealed class TableAggregate
{
    private readonly List<Card> _shoe = [];

    private readonly Dictionary<Guid, Player> _players = [];

    public IResult<IEnumerable<IDomainEvent>> CreateShoe(int numberOfDecks, int randomSeed)
    {
        List<Card> deck = (
            from rank in Enum.GetValues<Rank>()
            from suit in Enum.GetValues<Suit>()
            select new Card
            {
                Rank = rank,
                Suit = suit
            }).ToList();

        for (int i = 0; i < numberOfDecks; i++)
        {
            foreach (Card card in deck)
            {
                _shoe.Add(card);
            }
        }

        var random = new Random(randomSeed);

        random.Shuffle(CollectionsMarshal.AsSpan(_shoe));
    }

    public IResult<IEnumerable<IDomainEvent>> AddPlayer(Guid id, string name)
    {
        if (_players.ContainsKey(id))
        {
            return Result<IEnumerable<IDomainEvent>>.DomainError("Player already at table");
        }

        _players.Add(id, new Player(id, name));

        return Result.Success(new List<IDomainEvent>());
    }

    public IResult<IEnumerable<IDomainEvent>> HitPlayer(Guid playerId, Guid handId)
    {

    }

    public bool ContainsPlayer(Guid playerId) => _players.ContainsKey(playerId);
}
