#include "Player.h"

Player::Player()
{}

Player::Player(int initialWallet, Hand hand)
: wallet(initialWallet)
{
    this->numberOfHands = 1;
    this->hands[0] = hand;
}

void Player::addToHand(int n, Card card)
{
    this->hands[n].addCard(card);
}

void Player::splitHand(int n, Card card1, Card card2)
{
    this->hands[this->numberOfHands++] = this->hands[n].split(card1, card2);
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

void Player::resetHands()
{
    for (int i = 0; i <= this->numberOfHands; i++)
        this->hands[i].reset();
}