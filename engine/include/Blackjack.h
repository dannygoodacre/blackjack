#include <string>
#include <vector>

namespace Blackjack
{
    // Initialise the game.
    void setupGame(int initialPlayerWallet);

    // Deal cards to player and dealer.
    void startRound(int bet);

    // Draw a card.
    void hit(int n);

    // Draw no more cards and end the round.
    void stand(int n);

    // Double your bet and draw one more card.
    void doubleDown(int n);

    // Split the hand, drawing a new card to each.
    void split(int n);

    std::vector<std::string> getPlayerHand(int n);

    std::vector<std::string> getDealerHand();

    int getPlayerHandScore(int n);

    int getDealerHandScore();

    bool getIsRoundInProgress();

    // Return possible moves, or an empty vector if round not in progress.
    std::vector<char> getPossibleMoves(int n);

    // Return the result of the round, or 0 if in progress.
    char getOutcome(int n);

    int getPlayerWallet();

    int getNumberOfWins();

    int getNumberOfLosses();

    int getNumberOfDraws();

    int getNumberOfHands();
}