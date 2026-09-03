using System.Runtime.InteropServices;

namespace Blackjack;

public sealed class Shoe
{
    private List<Card> Cards { get; } = [];

    public Shoe(int numberOfDecks, int randomSeed)
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
                Cards.Add(card);
            }
        }

        var random = new Random(randomSeed);

        random.Shuffle(CollectionsMarshal.AsSpan(Cards));
    }

    public Card Draw()
    {
        Card card = Cards[^1];

        Cards.RemoveAt(Cards.Count - 1);

        return card;
    }
}
