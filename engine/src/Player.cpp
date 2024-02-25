#include "Player.h"

Player::Player()
{}

void Player::addToHand(int n, Card card)
{
    this->hands[n].addCard(card);
}

void Player::splitHand(int n)
{
    this->hands[this->numberOfHands++] = this->hands[n].split();
}

Hand Player::getHand(int n)
{
    return this->hands[n];
}

int Player::getHandScore(int n)
{
    return this->hands[n].getScore();
}

int Player::getWallet()
{
    return this->wallet;
}

void Player::setWallet(int n)
{
    this->wallet = n;
}

void Player::setNumberOfHands(int n)
{
    this->numberOfHands = n;
}

void Player::resetHands()
{
    for (int i = 0; i < this->numberOfHands; i++)
        this->hands[i].reset();
}

int Player::getNumberOfHands()
{
    return this->numberOfHands;
}
