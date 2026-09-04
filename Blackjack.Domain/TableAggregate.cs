using System.Runtime.InteropServices;
using DannyGoodacre.Primitives;

namespace Blackjack.Domain;

public sealed class TableAggregate
{
    private List<Card> Shoe { get; set; } = [];

    private List<Player> Players { get; set; } = [];

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

    public

    public bool ContainsPlayer(Guid playerId) => Players.Exists(x => x.Id == playerId);

    // TODO: Method to add player to table.
}
