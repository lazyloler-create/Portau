using System;
using Portau.Classes;

namespace Portau.Classes;

public class Deck
{
    private readonly List<Cards> cards = new List<Cards>();

    public Deck(List<string> Suit, List<string> Ranks)
    {
        foreach (var suit in Suit)
        {
            foreach (var rank in Ranks)
            {
                if(rank == Ranks[13]) {
                    continue;
                }
                cards.Add(new Cards([suit], [rank]));
            }
        }

        for(int i = 0; i < 4; i++)
        {
            cards.Add(new Cards());
        }
    }
    public void ShuffleDeck()
    {
       Random rng = new();
       for(int i = cards.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            Cards temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }   
    }

    public List<Cards> GetCards()
    {
        return cards;
    }
    public void DisplayDeck()
    {
        List<Cards> deck = GetCards();
        int j = 0;
        for(int i = 0; i < deck.Count; i++){
            Console.Write(deck[i] + " ");
            j++;
            if(j == 10)
            {
                Console.WriteLine();
                j = 0;
            }
        }
    }
}
