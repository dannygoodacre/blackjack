using DannyGoodacre.Primitives;

namespace Blackjack.Domain;

public sealed class TableAggregate(Guid id)
{
    private const int MaxPlayerCount = 7;

    private Guid Id { get; } = id;

    private string? Name { get; set; }

    private readonly List<Card> _shoe = [];

    private readonly List<Player> _players = [];

    public static (TableAggregate aggregate, TableCreatedEvent @event) Create(Guid id, string name)
    {
        var @event = new TableCreatedEvent(name);

        var aggregate = new TableAggregate(id);

        aggregate.Apply(@event);

        return (aggregate, @event);
    }

    // public IResult<List<IDomainEvent>> CreateShoe(int numberOfDecks, int randomSeed)
    // {
    //     List<Card> deck = (
    //         from rank in Enum.GetValues<Rank>()
    //         from suit in Enum.GetValues<Suit>()
    //         select new Card
    //         {
    //             Rank = rank,
    //             Suit = suit
    //         }).ToList();
    //
    //     for (int i = 0; i < numberOfDecks; i++)
    //     {
    //         foreach (Card card in deck)
    //         {
    //             _shoe.Add(card);
    //         }
    //     }
    //
    //     var random = new Random(randomSeed);
    //
    //     random.Shuffle(CollectionsMarshal.AsSpan(_shoe));
    //
    //     return Result.Success(new List<IDomainEvent>());
    // }

    public IResult<List<IDomainEvent>> AddPlayer(Guid playerId, string name)
    {
        if (_players.Count == MaxPlayerCount)
        {
            return new DomainError<List<IDomainEvent>>($"Table '{Id}' is full");
        }

        if (_players.Any(x => x.Id == playerId))
        {
            return Result<List<IDomainEvent>>.DomainError($"Player '{playerId}' is already at table '{Id}'");
        }

        var result = PlayerName.Create(name);

        if (result is not Success<PlayerName>(var playerName))
        {
            return result.MapFailure<List<IDomainEvent>>();
        }

        var @event = new PlayerAddedEvent(playerId, playerName);

        Apply(@event);

        return new Success<List<IDomainEvent>>([@event]);
    }

    private void Apply(TableCreatedEvent @event) => Name = @event.Name;

    private void Apply(PlayerAddedEvent @event)
        => _players.Add(new Player(@event.PlayerId, @event.Name));
}
