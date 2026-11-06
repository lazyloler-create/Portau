using System;

namespace Portau.Classes;

public class Game
{
    public int CoinFlip()
    {
        Random rng = new();
        string result = rng.Next(2) == 0 ? "Heads" : "Tails";

        Console.WriteLine($"Coin landed on: {result}");
        
        return result == "Heads" ? 1 : 2;        
    }

    public void Deal(List<string> P1, List<string>P2, Deck deck)
    {
        var cards = deck.GetCards();
        for(int i = cards.Count - 2; )
        {
            for(int j = 0; j < 4; i++)
            {
                
            }
        }
    }
}
