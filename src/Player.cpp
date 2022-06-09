#include "Header.h"

Player::Player(int startingWallet)
{
	this->wallet = startingWallet;
}

void Player::resetHand()
{
	this->hand.reset();
}

void Player::hit(Card card)
{
	this->hand.addCard(card);
}

void Player::addToWallet(int val)
{
	this->wallet += val;
}

Hand Player::getHand()
{
	return this->hand;
}

int Player::getWallet()
{
	return this->wallet;
}