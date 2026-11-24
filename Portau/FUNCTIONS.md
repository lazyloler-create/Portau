# Function Guide

## Main window

- `MainWindow()` initializes the window, records the hand-card placeholders, and shuffles a deck.
- `OnSettingsClick(...)` opens settings and connects its save, restart, and reset events to the main window.
- `OnDeckClick(...)` deals four cards to each player, displays them in the board slots, and disables the deck button.
- `OnBoardSlotClick(...)` moves a dealt card from a board slot to the hand position matching its rank.
- `ShowCards(...)` loads card images and displays them in the supplied slots.
- `ShuffleDeck()` creates a shuffled deck and enables dealing for a new round.
- `ClearBoard()` empties the board slots and restores the hand's original placeholders.
- `RestartRound()` clears the board and prepares a new shuffled round.
- `ResetGame()` restarts the round and resets both scores to zero.

## Deck

- `GetCards()` returns a copy of the complete card list.
- `CreateDeck()` returns a shuffled copy of the card list.
- `DealCards(...)` removes cards from a deck and distributes them between two players.
- `HasJokers(...)` checks whether a deck contains any jokers.

## Settings window

- `SettingsWindow()` creates settings with default player names.
- `SettingsWindow(player1Name, player2Name)` initializes settings with the supplied names.
- `OnSwapPlayersClick(...)` swaps the two name fields.als four cards to each pck and enables dealing for a new round.
- `OnSaveClick(...)` raises the save event with the entered names and closes the window.
- `OnCancelClick(...)` closes settings without saving.
- `OnRestartClick(...)` raises the restart event.
- `OnResetGameClick(...)` restores default names, raises save and reset events, then closes settings.
- `OnCardListClick(...)` displays information about the card list.
- `OnHelpClick(...)` displays the help message.
- `ShowInformation(...)` opens a small dialog containing the supplied title and message.
