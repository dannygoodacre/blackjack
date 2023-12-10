#include <vector>
#include <string>

namespace CLI
{
    int getStartingWallet();

    int getBet(int currentWallet);

    int getMenuChoice();

    char getMove(std::vector<char> possibleMoves);
}