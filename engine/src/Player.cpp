#include "Player.h"

Player::Player()
{}

Player::Player(int initialWallet, Hand hand)
: wallet(initialWallet), hand(hand) {}

void Player::addToHand(Card card)
{
    this->hand.addCard(card);
}

Hand Player::getHand()
{
    return this->hand;
}

int Player::getHandScore()
{
    return this->hand.getScore();
}

int Player::getWallet()
{
    return this->wallet;
}

void Player::setWallet(int n)
{
    this->wallet = n;
}

void Player::resetHand()
{
    this->hand.reset();
}