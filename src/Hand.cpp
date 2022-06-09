#include "Header.h"

Hand::Hand()
{
	this->reset();
}

void Hand::reset()
{
	// empty the hand
	for (int i = 0; i < 14; i++)
	{
		this->cards.at(i) = Card();
	}

	this->score = 0;
	this->numberOfCards = 0;
	this->numberOfAces = 0;
}

void Hand::addCard(Card card)
{
	this->cards.at(this->numberOfCards) = card;
	this->numberOfCards++;

	if (Card::scoreOf(card) == 11)
	{
		// card is an ace
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

bool Hand::isBlackjack()
{
	// 2 cards in hand and score is 21
	return this->numberOfCards == 2 && this->getScore() == 21;
}

bool Hand::isBust()
{
	return this->getScore() > 21;
}

int Hand::getScore()
{
	return this->score;
}

void Hand::showScore()
{
	std::string out = (this->isBlackjack()) ?
		" (Blackjack)" : (" (" + std::to_string(this->score) + ")");
	std::cout << out << "\n\n";
}
void Hand::show()
{
	for (int i = 0; i < this->numberOfCards; i++)
	{
		Card::print(this->cards[i]);
		std::cout << " ";
	}
}

Card Hand::getCard(int n)
{
	return this->cards.at(n);
}