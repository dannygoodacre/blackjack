#pragma once

#include "Card.h"
#include "Hand.h"

class Player
{
public:
    Player();
    Player(int initialWallet, Hand hand);

    void addToHand(Card card);
    Hand getHand();
    int getHandScore();
    int getWallet();
    void setWallet(int n);

private:
    Hand hand;
    int wallet;
};