#include "Header.h"

Player::Player(int startingWallet)
{
	this->wallet = startingWallet;
}

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

int Player::getWallet()
{
	return this->wallet;
}

void Player::addToWallet(int val)
{
	this->wallet += val;
}