using System;
using System.Collections.Generic;

namespace Portau;

public class Deck
{
    private static readonly List<string> DeckCards =
    [
        "Cards/AH.png", "Cards/AD.png", "Cards/AC.png", "Cards/AS.png",
        "Cards/2H.png", "Cards/2D.png", "Cards/2C.png", "Cards/2S.png",
        "Cards/3H.png", "Cards/3D.png", "Cards/3C.png", "Cards/3S.png",
        "Cards/4H.png", "Cards/4D.png", "Cards/4C.png", "Cards/4S.png",
        "Cards/5H.png", "Cards/5D.png", "Cards/5C.png", "Cards/5S.png",
        "Cards/6H.png", "Cards/6D.png", "Cards/6C.png", "Cards/6S.png",
        "Cards/7H.png", "Cards/7D.png", "Cards/7C.png", "Cards/7S.png",
        "Cards/8H.png", "Cards/8D.png", "Cards/8C.png", "Cards/8S.png",
        "Cards/9H.png", "Cards/9D.png", "Cards/9C.png", "Cards/9S.png",
        "Cards/10H.png", "Cards/10D.png", "Cards/10C.png", "Cards/10S.png",
        "Cards/JH.png", "Cards/JD.png", "Cards/JC.png", "Cards/JS.png",
        "Cards/QH.png", "Cards/QD.png", "Cards/QC.png", "Cards/QS.png",
        "Cards/KH.png", "Cards/KD.png", "Cards/KC.png", "Cards/KS.png"
    ];

    public List<string> GetCards()
    {
        return DeckCards;
    }
    
    
    public List<string> CreateDeck()
    {
        var deck = new List<string>(DeckCards);
        for (int i = deck.Count - 1; i > 0; i--)
        {   
            int j = Random.Shared.Next(i + 1);
            (deck[i], deck[j]) = (deck[j], deck[i]);
        }

        return deck;
    }
    
    public void DealCards(List<string> deck, List<string> player1, List<string> player2, int cardsPerPlayer = 4)
    {
        ArgumentNullException.ThrowIfNull(deck);
        ArgumentNullException.ThrowIfNull(player1);
        ArgumentNullException.ThrowIfNull(player2);

        if (cardsPerPlayer < 0 || cardsPerPlayer > 5 || cardsPerPlayer < 4)
        {
            cardsPerPlayer = 4;
        }

        if (deck.Count < cardsPerPlayer * 2)
            throw new ArgumentException("There are not enough cards in the deck to deal to both players!");

        for (var i = 0; i < cardsPerPlayer; i++)
        {
            player1.Add(deck[0]);
            deck.RemoveAt(0);
            player2.Add(deck[0]);
            deck.RemoveAt(0);
        }
    }
}