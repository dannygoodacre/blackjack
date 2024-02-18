#pragma once

#include <string>
#include <iostream>

class Card
{
public:
    static const std::string RANKS[13];
    static const std::string SUITS[4];

    Card(std::string rank = "0", std::string suit = "x");

    std::string getRank();
    std::string getSuit();
    int getScore();

    bool isAce();

    friend std::ostream& operator<<(std::ostream& os, const Card& card);
    friend bool operator==(const Card& lhs, const Card& rhs);

private:
    std::string rank;
    std::string suit;
    int score;

    static bool isNumber(std::string& s);
    static int calculateScore(std::string rank);
};