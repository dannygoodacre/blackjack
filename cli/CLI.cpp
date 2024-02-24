#include "CLI.h"
#include "../engine/include/Blackjack.h"

#include <string>
#include <iostream>
#include <algorithm>
#include <climits>

using namespace Blackjack;

bool stringToInt(int& i, const char *s)
{
    char *end;
    long n = strtol(s, &end, 10);
    if (n > INT_MAX || n < INT_MIN || *s == '\0' || *end != '\0')
        return false;
    i = n;

    return true;
}

int readInt(std::string prompt)
{
    std::string input;
    int n;

    do
    {
        std::cout << prompt;
        std::getline(std::cin, input);
    }
    while (!stringToInt(n, input.c_str()));

    return n;
}

char readChar(std::string prompt)
{
    std::string input;

    do
    {
        std::cout << prompt;
        std::getline(std::cin, input);
    }
    while (input.length() != 1 && std::isalpha(input[0]));

    return input[0];
}

int CLI::getStartingWallet()
{
    int n;
    while ((n = readInt("Starting wallet: ")) <= 0);
    return n;
}

int CLI::getBet(int currentWallet)
{
    int n;
    while ((n = readInt("Bet (" + std::to_string(currentWallet) + "): ")) > currentWallet || n <= 0);
    return n;
}

int CLI::getMenuChoice()
{
    std::cout << "0 - exit\n1 - start round\n";
    int n;
    while ((n = readInt("Choice: ")) != 0 && n != 1);
    return n;
}

char CLI::getMove(std::vector<char> possibleMoves)
{
    for (const auto& move : possibleMoves)
    {
        switch (move)
        {
            case 'H':
                std::cout << "H - hit\n";
                break;
            case 'S':
                std::cout << "S - stand\n";
                break;
            case 'D':
                std::cout << "D - double down\n";
        }
    }

    char c;
    do
        c = readChar("Choice: ");
    while (!std::count(possibleMoves.begin(), possibleMoves.end(), c));

    return c;
}

void CLI::showOutcomes()
{
    std::cout << "W/L/D : " << getNumberOfWins() << "/" << getNumberOfLosses() << "/" << getNumberOfDraws() << "\n";
}

void CLI::showPlayerHands(int handInPlay)
{
    std::cout << "Player:\n";
    for (int i = 0; i < getNumberOfHands(); i++)
    {
        std::vector<std::string> hand = getPlayerHand(i);

        for (int j = 0; j < hand.size(); j++)
            std::cout << hand[j] << " ";
        std::cout << "(" << getPlayerHandScore(i) << ")" << (handInPlay == i ? " <-" : "") << "\n";
    }
}

void CLI::showDealerHand(bool isRoundOver)
{
    std::vector<std::string> hand = getDealerHand();

    std::cout << "Dealer: ";
    for (int i = 0; i < hand.size(); i++)
        std::cout << (i == 1 && !isRoundOver ? "XX" : hand[i]) << " ";

    if (isRoundOver)
        std::cout << "(" << getDealerHandScore() << ")";
    std::cout << std::endl;
}

bool CLI::roundLoop()
{
    std::cout << "-------------------------\n";
    int bet = CLI::getBet(getPlayerWallet());
    startRound(bet);

    bool again;
    if (getOutcome())
        again = false;
    else
        again = true;

    while (again)
    {
        CLI::showHand("\nDealer", getDealerHand(), false, true);
        CLI::showHand("Player", getPlayerHand(), true);

        switch (CLI::getMove(getPossibleMoves()))
        {
            case 'H':
                hit();
                again = getIsRoundInProgress();
                break;
            case 'S':
                stand();
                again = false;
                break;
            case 'D':
                doubleDown();
                again = false;
                break;
        }
    }

    char outcome = getOutcome();
    std::cout << "\n";
    CLI::showHand("Dealer", getDealerHand(), true);
    CLI::showHand("Player", getPlayerHand(), true);
    std::cout << "Outcome: " << outcome << std::endl;
    std::cout << "W/L/D: " << getNumberOfWins() << "/" << getNumberOfLosses() << "/" << getNumberOfDraws();
    std::cout << "\n\n";

    return !getPlayerWallet();
}