#pragma once

#include "Card.h"

class Shoe
{
public:
    Shoe();

    Card drawCard();
    void shuffle();
    
private:
    Card cards[416];
    int top;
};