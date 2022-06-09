#include "Header.h"

bool Hand::isBlackjack(Hand hand)
{
	// 3rd card empty, so only 2 cards in hand, and score = 21
	return (hand.cards[2].getRank() == "0" && hand.getScore() == 21);
}

Hand::Hand()
{
	this->reset();
}

void Hand::addCard(Card card)
{
	this->cards.at(this->numberOfCards) = card;
	this->numberOfCards++;

	if (Card::scoreOf(card) == 11) // card is an ace
	{
		this->numberOfAces++;
	}

	this->score += Card::scoreOf(card);

	if (this->score > 21 && this->numberOfAces > 0)
	{
		// there are aces which can be 'softened'
		this->score -= 10;
		this->numberOfAces--;
	}
}

int Hand::getScore()
{
	return this->score;
}

bool Hand::isBust()
{
	return this->getScore() > 21;
}

void Hand::show()
{
	for (int i = 0; i < this->numberOfCards; i++)
	{
		Card::print(this->cards[i]);
		std::cout << " ";
	}
}

void Hand::reset()
{
	for (int i = 0; i < 14; i++)
	{
		this->cards.at(i) = Card();
	}

	this->score = 0;
	this->numberOfCards = 0;
	this->numberOfAces = 0;
}

Card Hand::getCard(int n)
{
	return this->cards.at(n);
}