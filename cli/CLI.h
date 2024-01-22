#include <vector>
#include <string>

namespace CLI
{
    int getStartingWallet();

    int getBet(int currentWallet);

    int getMenuChoice();

    char getMove(std::vector<char> possibleMoves);

    void showHand(std::string name, std::vector<std::string> hand, bool hideScore, bool hideSecondCard = false);

    bool roundLoop();
}