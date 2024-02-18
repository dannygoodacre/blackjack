#pragma once

#include "Card.h"

#include <vector>
#include <initializer_list>
#include <iostream>

class Hand
{
public:
    Hand();
    Hand(std::initializer_list<Card> cards);

    void reset();

    void addCard(Card card);
    Hand split();

    bool isBlackjack();
    bool isBust();
    bool isSoft();

    int getScore();
    int getNumberOfCards();
    Card getCardAt(int n);

    friend std::ostream& operator<<(std::ostream& os, const Hand& hand);

private:
    std::vector<Card> cards;

    int score;
    int numberOfCards;
    int numberOfSoftAces;
    bool bust;

    void updateScore(Card card);
};