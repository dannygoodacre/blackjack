using System.Runtime.InteropServices;
using DannyGoodacre.Primitives;

namespace Blackjack.Domain;

public sealed class TableAggregate(Guid id)
{
    public Guid Id { get; init; } = id;

    private readonly List<Card> _shoe = [];

    private readonly Dictionary<Guid, Player> _players = [];

    public IResult<List<IDomainEvent>> CreateShoe(int numberOfDecks, int randomSeed)
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

        return Result.Success(new List<IDomainEvent>());
    }

    public IResult<List<IDomainEvent>> AddPlayer(Guid playerId, string name)
    {
        if (_players.ContainsKey(playerId))
        {
            return Result<List<IDomainEvent>>.DomainError("Player already at table");
        }

        _players.Add(playerId, new Player(playerId, name));

        var @event = new PlayerAdded(playerId, name);

        return Result.Success(new List<IDomainEvent> { @event });
    }

    public bool ContainsPlayer(Guid playerId) => _players.ContainsKey(playerId);
}
