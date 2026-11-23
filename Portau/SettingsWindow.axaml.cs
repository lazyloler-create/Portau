using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Portau;

public partial class SettingsWindow : Window
{
    public event Action<string, string>? Saved;
    public event Action? RestartRequested;
    public event Action? ResetGameRequested;

    public SettingsWindow() : this("Player 1", "Player 2")
    {
    }

    public SettingsWindow(string player1Name, string player2Name)
    {
        InitializeComponent();
        Player1NameInput.Text = player1Name;
        Player2NameInput.Text = player2Name;
    }

    private void OnSwapPlayersClick(object? sender, RoutedEventArgs e)
    {
        (Player1NameInput.Text, Player2NameInput.Text) =
            (Player2NameInput.Text, Player1NameInput.Text);
    }

    private void OnSaveClick(object? sender, RoutedEventArgs e)
    {
        Saved?.Invoke(Player1NameInput.Text?.Trim() ?? string.Empty,
            Player2NameInput.Text?.Trim() ?? string.Empty);
        Close();
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void OnRestartClick(object? sender, RoutedEventArgs e)
    {
        RestartRequested?.Invoke();
    }

    private void OnResetGameClick(object? sender, RoutedEventArgs e)
    {
        Player1NameInput.Text = "Player 1";
        Player2NameInput.Text = "Player 2";
        Saved?.Invoke("Player 1", "Player 2");
        ResetGameRequested?.Invoke();
        Close();
    }

    private async void OnCardListClick(object? sender, RoutedEventArgs e)
    {
        await ShowInformation("Card list", "The game board currently has 13 cards.");
    }

    private async void OnHelpClick(object? sender, RoutedEventArgs e)
    {
        await ShowInformation("Help", "Set player names, swap players, or restart the round. Reset Game restores the default player names and clears the board.");
    }

    private async System.Threading.Tasks.Task ShowInformation(string title, string message)
    {
        var informationWindow = new Window
        {
            Title = title,
            Width = 320,
            Height = 160,
            CanResize = false,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            Content = new TextBlock
            {
                Text = message,
                TextWrapping = Avalonia.Media.TextWrapping.Wrap,
                Margin = new Avalonia.Thickness(20),
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
            }
        };

        await informationWindow.ShowDialog(this);
    }
}
