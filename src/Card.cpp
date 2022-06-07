#include "Header.h"

const std::string Card::RANKS[13] = { "A","2","3","4","5","6","7","8","9","10","J","Q","K" };
const char Card::SUITS[4] = { 'h','d','c','s' };

bool Card::isNumber(const std::string& s)
{
	return !s.empty() && find_if(s.begin(), s.end(), [](unsigned char c) { return !isdigit(c); }) == s.end();
}

int Card::scoreOf(Card card)
{
	std::string rank = card.getRank();

	if (isNumber(rank) && stoi(rank) > 1 && stoi(rank) < 11)
	{
		return stoi(rank); // 2 to 10
	}
	else
	{
		switch ((int) rank[0])
		{
		case 65: // A
			return 11;
			break;
		case 74: // J
		case 75: // K
		case 81: // Q
			return 10;
			break;
		default: // rank is not valid
			return -1;
		}
	}
}

void Card::print(Card card)
{
	std::cout << card.getRank() + card.getSuit();
}

Card::Card() // empty card
{
	this->rank = "0";
	this->suit = '0';
}

Card::Card(std::string rank, char suit)
{
	this->rank = rank;
	this->suit = suit;
}

std::string Card::getRank()
{
	return this->rank;
}

char Card::getSuit()
{
	return this->suit;
}