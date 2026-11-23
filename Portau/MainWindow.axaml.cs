using System;
using System.Collections.Generic;
using Avalonia.Automation;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Portau;

public partial class MainWindow : Window
{
    private readonly Deck _cardDeck = new();
    private List<string> _cardsRemaining = [];
    private bool _cardsDealt;

    public MainWindow()
    {
        InitializeComponent();
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

        var player1Cards = new List<string>();
        var player2Cards = new List<string>();
        _cardDeck.DealCards(_cardsRemaining, player1Cards, player2Cards);

        ShowCards(player1Cards, BottomSlot1, BottomSlot2, BottomSlot3, BottomSlot4);
        ShowCards(player2Cards, TopSlot1, TopSlot2, TopSlot3, TopSlot4);
        _cardsDealt = true;
        DeckButton.IsEnabled = false;
    }

    private void OnHandCardClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button handCard || handCard.Content is not Image image)
        {
            return;
        }
        var card = AutomationProperties.GetName(handCard);

        Button? target = card switch
        {
            "Ace of Hearts" or "Ace of Diamonds" or "Ace of Clubs" or "Ace of Spades" => AceOfHearts,
            "2 of Hearts" or "2 of Diamonds" or "2 of Clubs" or "2 of Spades" => TwoOfHearts,
            "3 of Hearts" or "3 of Diamonds" or "3 of Clubs" or "3 of Spades" => ThreeOfHearts,
            "4 of Hearts" or "4 of Diamonds" or "4 of Clubs" or "4 of Spades" => FourOfHearts,
            "5 of Hearts" or "5 of Diamonds" or "5 of Clubs" or "5 of Spades" => FiveOfHearts,
            "6 of Hearts" or "6 of Diamonds" or "6 of Clubs" or "6 of Spades" => SixOfHearts,
            "7 of Hearts" or "7 of Diamonds" or "7 of Clubs" or "7 of Spades" => SevenOfHearts,
            "8 of Hearts" or "8 of Diamonds" or "8 of Clubs" or "8 of Spades" => EightOfHearts,
            "9 of Hearts" or "9 of Diamonds" or "9 of Clubs" or "9 of Spades" => NineOfHearts,
            "10 of Hearts" or "10 of Diamonds" or "10 of Clubs" or "10 of Spades" => TenOfHearts,
            "Jack of Hearts" or "Jack of Diamonds" or "Jack of Clubs" or "Jack of Spades" => JackOfHearts,
            "Queen of Hearts" or "Queen of Diamonds" or "Queen of Clubs" or "Queen of Spades" => QueenOfHearts,
            "King of Hearts" or "King of Diamonds" or "King of Clubs" or "King of Spades" => KingOfHearts,
            _ => null
        };

        if (target != null)
        {
            target.Content = new Image
            {
                Source = image.Source, Stretch = Avalonia.Media.Stretch.Uniform
            };
        }
    }

    private static void ShowCards(IReadOnlyList<string> cards, params Button[] slots)
    {
        for (var i = 0; i < slots.Length; i++)
        {
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

    private void ShuffleDeck()
    {
        _cardsRemaining = _cardDeck.CreateDeck();
        _cardsDealt = false;
        
        bool hasJokers = _cardDeck.HasJokers(_cardsRemaining);
        if (!hasJokers)
        {
            Console.WriteLine("No jokers found in the deck.");
        }

        DeckButton.IsEnabled = true;
    }

    private void ClearBoard()
    {
        TopSlot1.Content = null;
        TopSlot2.Content = null;
        TopSlot3.Content = null;
        TopSlot4.Content = null;
        BottomSlot1.Content = null;
        BottomSlot2.Content = null;
        BottomSlot3.Content = null;
        BottomSlot4.Content = null;
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