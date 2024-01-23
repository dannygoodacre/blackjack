#include "../engine/include/Blackjack.h"
#include "CLI.h"

#include <iostream>
#include <vector>
#include <string>

int main()
{
    Blackjack::setupGame(CLI::getStartingWallet());
    std::cout<< "\n";

    bool isBroke;
    int choice;
    do
    {
        switch (choice = CLI::getMenuChoice())
        {
            case 1:
                isBroke = CLI::roundLoop();
                break;
        }

        if (isBroke)
        {
            std::cout << "You ran out of funds, goodbye!\n";
            return 0;
        }
    }
    while (choice != 0);

    std::cout << "\nGoodbye!\n";
    return 0;
}