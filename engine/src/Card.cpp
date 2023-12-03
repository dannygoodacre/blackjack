#include "Card.h"

#include <string>
#include <algorithm>

const std::string Card::RANKS[13]
	= { "A","2","3","4","5","6","7","8","9","10","J","Q","K" };
const std::string Card::SUITS[4] = { "h","d","c","s" };

Card::Card(std::string rank, std::string suit)
: rank(rank), suit(suit), score(Card::calculateScore(rank))
{}

std::string Card::getRank()
{
    return this->rank;
}

std::string Card::getSuit()
{
    return this->suit;
}

int Card::getScore()
{
    return this->score;
}

bool Card::isAce()
{
    return this->rank == "A";
}

std::ostream& operator<<(std::ostream& os, const Card& card)
{
    os << card.rank << card.suit;
    return os;
}

bool Card::isNumber(std::string& s)
{
	return !s.empty() && find_if(s.begin(), s.end(),
		[](unsigned char c) { return !isdigit(c); }) == s.end();
}

int Card::calculateScore(std::string rank)
{
    if (isNumber(rank) && stoi(rank) > 1 && stoi(rank) < 11)
        return stoi(rank);
    
    switch ((int) rank[0])
    {
        case 'A':
            return 11;
        case 'J':
        case 'Q':
        case 'K':
            return 10;
    }

    return 0;
}

