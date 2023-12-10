#include "CLI.h"

#include <string>
#include <iostream>
#include <algorithm>
#include <climits>

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
    while ((n = readInt("Start: ")) <= 0);
    return n;
}

int CLI::getBet(int currentWallet)
{
    int n;
    while ((n = readInt("Bet: ")) > currentWallet || n <= 0);
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