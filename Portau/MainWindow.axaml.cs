using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Portau;

public partial class MainWindow : Window
{
    // creates the deck used to create and deal cards.
    private static Deck _cardDeck = new();
    // holds the cards still available to deal.
    private List<string> _cardsRemaining = [];
    // tracks whether cards have been dealt for this round
    private bool _cardsDealt;
    // stores the buttons representing each rank in the hand row
    private readonly List<Button> _handCards;
    // saves the hand buttons' original contents so they can be restored on restart
    private readonly List<object?> _handCardPlaceholders;
    // players hands of cards
    private static List<string> _player1Cards = new List<string>();
    private static List<string> _player2Cards = new List<string>();

    public MainWindow()
    {
        InitializeComponent();
        _handCards =
        [
            AceOfHearts, TwoOfHearts, ThreeOfHearts, FourOfHearts, FiveOfHearts,
            SixOfHearts, SevenOfHearts, EightOfHearts, NineOfHearts, TenOfHearts,
            JackOfHearts, QueenOfHearts, KingOfHearts
        ];
        _handCardPlaceholders = _handCards.ConvertAll(handCard => handCard.Content);
        ShuffleDeck();
    }
    
    private async void OnSettingsClick(object? sender, RoutedEventArgs e)
    {
        var settingsWindow = new SettingsWindow(Player1Label.Text ?? "Player 1", Player2Label.Text ?? "Player 2");
        settingsWindow.Saved += (player1, player2) =>
        {
            Player1Label.Text = player1;
            Player2Label.Text = player2;
        };
        settingsWindow.RestartRequested += RestartRound;
        settingsWindow.ResetGameRequested += ResetGame;

        await settingsWindow.ShowDialog(this);
    }
    
   
    private void OnDeckClick(object? sender, RoutedEventArgs e)
    {
        if (_cardsDealt)
            return;

       
        _cardDeck.DealCards(_cardsRemaining, _player1Cards, _player2Cards);

        ShowCards(_player1Cards, BottomSlot1, BottomSlot2, BottomSlot3, BottomSlot4);
        ShowCards(_player2Cards, TopSlot1, TopSlot2, TopSlot3, TopSlot4);
        _cardsDealt = true;
        DeckButton.IsEnabled = false;
    }

    // Moves the clicked dealt card into the hand position matching its rank --Ai assisted code
    private void OnBoardSlotClick(object? sender, RoutedEventArgs e)
    {
        if (!_cardsDealt || 
            sender is not Button boardSlot ||
            boardSlot.Tag is not string cardPath ||
            boardSlot.Content is not Image cardImage)
        {
            return;
        }

        var rank = Path.GetFileNameWithoutExtension(cardPath);
        if (rank == "Joker")
            return; //later will add for adding a value to joker based on user input

        var rankPrefix = rank.Length > 2 ? rank[..^1] : rank[..1];
        var handIndex = rankPrefix switch
        {
            "A" => 0,
            "2" => 1,
            "3" => 2,
            "4" => 3,
            "5" => 4,
            "6" => 5,
            "7" => 6,
            "8" => 7,
            "9" => 8,
            "10" => 9,
            "J" => 10,
            "Q" => 11,
            "K" => 12,
            _ => -1
        };
        if (handIndex < 0)
            return;

        _handCards[handIndex].Content = new Image
        {
            Source = cardImage.Source,
            Stretch = Avalonia.Media.Stretch.Uniform
        };
        boardSlot.Content = null;
        boardSlot.Tag = null;
    }

    private static void ShowCards(IReadOnlyList<string> cards, params Button[] slots)
    {
        for (var i = 0; i < slots.Length; i++)
        {
            slots[i].Tag = cards[i];
            var asset = AssetLoader.Open(new Uri($"avares://Portau/{cards[i]}"));
            if (asset != null)
            {
                slots[i].Content = new Image
                {
                    Source = new Bitmap(asset),
                    Stretch = Avalonia.Media.Stretch.Uniform
                };
            }
            else
            {
                Console.WriteLine($"Resource not found: {cards[i]}");
            }
        }
    }

    private static int ReturnMissingIndex(List<string> hand)
    {
        int i = 0;
        foreach (var card in hand)
        {
            if (card is null)
                break;
            else
                i++;
        }
        return i;
    }
    
    private static void DealCard(List<string> deck, List<string> player, int missingIndex)
    {
        if (missingIndex >= 0 && missingIndex < deck.Count)
        {
            player.Insert(missingIndex, deck[0]);
            deck.RemoveAt(0);
        }
    }
    private static void DealMissing()
    {
        var p1 = ReturnMissingIndex(_player1Cards);
        var p2 = ReturnMissingIndex(_player2Cards);
        
        if (p1 != 0)
            DealCard(_cardDeck.GetCards(), _player1Cards, p1);
        if (p2 != 0)
            DealCard(_cardDeck.GetCards(),  _player2Cards, p2);
    }

    private void ShuffleDeck()
    {
        _cardsRemaining = _cardDeck.CreateDeck();
        _cardsDealt = false;
        DeckButton.IsEnabled = true;
    }

    private void ClearBoard()
    {
        TopSlot1.Content = null;
        TopSlot1.Tag = null;
        TopSlot2.Content = null;
        TopSlot2.Tag = null;
        TopSlot3.Content = null;
        TopSlot3.Tag = null;
        TopSlot4.Content = null;
        TopSlot4.Tag = null;
        BottomSlot1.Content = null;
        BottomSlot1.Tag = null;
        BottomSlot2.Content = null;
        BottomSlot2.Tag = null;
        BottomSlot3.Content = null;
        BottomSlot3.Tag = null;
        BottomSlot4.Content = null;
        BottomSlot4.Tag = null;
        for (var i = 0; i < _handCards.Count; i++)
            _handCards[i].Content = _handCardPlaceholders[i];
    }

    private void RestartRound()
    {
        ClearBoard();
        ShuffleDeck();
    }

    private void ResetGame()
    {
        RestartRound();
        Player1ScoreLabel.Text = "Points: 0";
        Player2ScoreLabel.Text = "Points: 0";
    }
}