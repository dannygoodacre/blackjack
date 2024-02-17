#pragma once

#include "Card.h"
#include "Hand.h"

#include <vector>

class Player
{
public:
    Player();
    Player(int initialWallet, Hand hand);

    void addToHand(int n, Card card);
    void splitHand(int n, Card card1, Card card2);
    Hand getHand(int n);
    int getHandScore(int n);
    int getWallet();
    void setWallet(int n);
    void resetHands();

private:
    Hand hands[4];
    int wallet;
    int numberOfHands;
};