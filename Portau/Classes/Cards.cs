namespace Portau.Classes;

public class Cards
{
    public List<string> Suit = new List<string>
    {
        "[♥", "[♦", "[♣", "[♠"
    };
    public List<string> Rank = new List<string>
    {
      "A]", "1]", "2]", "3]", "4]", "5]", "6]", "7]", "8]", "9]", "10]", "J]", "Q]", "K]"  
    };

    public Cards(List<string> suit, List<string> rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public Cards()
    {
        Suit.Clear();
        Rank[0] = "[J]";
    }

    public override string ToString()
    {
        return Suit.Count == 0 ? "[J]" : $"{Suit[0]}{Rank[0]}";
    }
}
