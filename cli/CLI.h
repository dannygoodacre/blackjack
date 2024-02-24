#include <vector>
#include <string>

namespace CLI
{
    int getStartingWallet();

    int getBet(int currentWallet);

    int getMenuChoice();

    char getMove(std::vector<char> possibleMoves);

    void showOutcomes();

    void showPlayerHands(int handInPlay = -1);

    void showDealerHand(bool isRoundOver = false);

    bool roundLoop();
}