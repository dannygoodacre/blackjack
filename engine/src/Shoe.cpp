#include "Shoe.h"

#include <chrono>
#include <random>
#include <algorithm>

Shoe::Shoe()
{
    Card deck[52];

    // Create deck.
    int k = 0;
    for (int i = 0; i < 4; i++)
        for (int j = 0; j < 13; j++)
            deck[k++] = Card(Card::RANKS[j], Card::SUITS[i]);
    
    // Populate shoe.
    for (int i = 0; i < 8; i++)
        for (int j = 0; j < 52; j++)
            this->cards[52*i + j] = deck[j];

    this->shuffle();
}

Card Shoe::drawCard()
{
    return this->cards[this->top++];
}

void Shoe::shuffle()
{
    unsigned seed = std::chrono::system_clock::now().time_since_epoch().count();
    std::shuffle(std::begin(this->cards), std::end(this->cards), 
        std::default_random_engine(seed));
    this->top = 0;
}