#pragma once

#include "Card.h"
#include "Hand.h"

#include <vector>

class Player
{
public:
    Player();

    void addToHand(int n, Card card);
    void splitHand(int n);
    Hand getHand(int n);
    int getHandScore(int n);
    int getWallet();
    void setWallet(int n);
    void setNumberOfHands(int n);
    void resetHands();
    int getNumberOfHands();

private:
    Hand hands[4];
    int wallet;
    int numberOfHands;
};