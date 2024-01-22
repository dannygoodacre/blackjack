#include "../engine/include/Blackjack.h"
#include "CLI.h"

#include <iostream>
#include <vector>
#include <string>

int main()
{
    Blackjack::setupGame(CLI::getStartingWallet());

    int choice;
    do
    {
        switch (choice = CLI::getMenuChoice())
        {
            case 1:
                CLI::roundLoop();
                break;
        }
        std::cout << "\n\n\n";
    }
    while (choice != 0);

    std::cout << "\n\nGoodbye!\n";
    return 0;
}