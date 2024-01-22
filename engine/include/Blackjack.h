#include <string>
#include <vector>

namespace Blackjack
{
    // Initialise the game.
    void setupGame(int initialPlayerWallet);

    // Deal cards to player and dealer.
    void startRound(int bet);

    // Draw a card.
    void hit();

    // Draw no more cards and end the round.
    void stand();

    // Double your bet and draw one more card.
    void doubleDown();

    std::vector<std::string> getPlayerHand();

    std::vector<std::string> getDealerHand();

    int getPlayerHandScore();

    int getDealerHandScore();

    bool getIsRoundInProgress();

    // Return possible moves, or an empty vector if round not in progress.
    std::vector<char> getPossibleMoves();

    // Return the result of the round, or 0 if in progress.
    char getOutcome();

    int getPlayerWallet();
}