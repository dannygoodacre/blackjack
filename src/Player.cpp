#include "Header.h"

void Player::hit(Card card)
{
	this->hand.addCard(card);
}

Hand Player::getHand()
{
	return this->hand;
}

void Player::resetHand()
{
	this->hand.reset();
}