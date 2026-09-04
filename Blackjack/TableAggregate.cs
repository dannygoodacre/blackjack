using System.Runtime.InteropServices;
using DannyGoodacre.Primitives;

namespace Blackjack;

public class TableAggregate
{
    public List<Card> Shoe { get; set; } = [];

    public string ActivePlayerId { get; private set; } = null!;

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
                Shoe.Add(card);
            }
        }

        var random = new Random(randomSeed);

        random.Shuffle(CollectionsMarshal.AsSpan(Shoe));
    }

    public IResult<IEnumerable<IDomainEvent>> Hit(string playerId)
    {
        if (ActivePlayerId != playerId)
        {
            return Result<IEnumerable<IDomainEvent>>.DomainError("Not your turn");
        }

        // draw card from shoe.

        var events = new List<IDomainEvent>
        {
            new CardDealtEvent("test", "test", playerId, new Card(), 1, 0)
        };

        return Result<IEnumerable<IDomainEvent>>.Success(events);
    }
}

public readonly record struct Card(Rank Rank, Suit Suit);

public enum Rank
{
    Ace = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13
}

public enum Suit
{
    Diamond,
    Heart,
    Club,
    Spade
}

public interface IDomainEvent
{
    Guid Id { get; set; }

    string StreamId { get; set; }

    DateTime TimestampUtc { get; set; }
}

public abstract record DomainEvent(string StreamId) : IDomainEvent
{

    public Guid Id { get; set; } = Guid.NewGuid();

    public string StreamId { get; set; } = StreamId;

    public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;
}

public record ShoeShuffledEvent(
    string StreamId,
    string RoundId,
    IReadOnlyList<Card> Cards
) : DomainEvent(StreamId);

public record CardDealtEvent(
    string StreamId,
    string RoundId,
    string PlayerId,
    Card Card,
    int HandTotal,
    int HandIndex = 0
) : DomainEvent(StreamId);
