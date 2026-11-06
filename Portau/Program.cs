using Portau.Classes;


namespace Portau.Main
{
    public class Program
    {
        public static void Main()
        {
        List<string> Suits = new List<string>
        {
            "[♥", "[♦", "[♣", "[♠"
        };
        List<string> Ranks = new List<string>
        {
            "A]", "1]", "2]", "3]", "4]", "5]", "6]", "7]", "8]", "9]", "10]", "J]", "Q]", "K]"  
        };

            Deck deck = new Deck(Suits, Ranks);
            deck.ShuffleDeck();
            deck.DisplayDeck();
        }
    }
}