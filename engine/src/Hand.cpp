#include "Card.h"
#include "Hand.h"

#include <vector>
#include <initializer_list>

Hand::Hand()
{}

Hand::Hand(std::initializer_list<Card> cards)
{
    this->reset();
    for (Card card : cards)
        this->addCard(card);
}

void Hand::reset()
{
    this->cards.clear();

    this->score = 0;
    this->numberOfCards = 0;
    this->numberOfSoftAces = 0;

    this->bust = false;
}

void Hand::addCard(Card card)
{
    this->cards.push_back(card);
    this->numberOfCards++;
    this->updateScore(card);
}

Hand Hand::split()
{
    Hand newHand({this->getCardAt(1)});
    this->cards.pop_back();
    this->numberOfCards--;

    return newHand;
}

bool Hand::isBlackjack()
{
    return this->score == 21 && this->numberOfCards == 2;
}

bool Hand::isBust()
{
    return this->bust;
}

bool Hand::isSoft()
{
    return this->numberOfSoftAces > 0;
}

int Hand::getScore()
{
    return this->score;
}

int Hand::getNumberOfCards()
{
    return this->numberOfCards;
}

Card Hand::getCardAt(int n)
{
    return this->cards.at(n);
}

std::ostream& operator<<(std::ostream& os, const Hand& hand)
{
    for (auto card : hand.cards)
        os << card << ",";
    os << '\b';
    return os;
}

void Hand::updateScore(Card card)
{
    this->score += card.getScore();

    if (card.isAce())
        this->numberOfSoftAces++;

    if (this->score > 21)
    {
        if (this->numberOfSoftAces > 0)
        {
            this->score -= 10;
            this->numberOfSoftAces--;
        }
        else
            this->bust = true;
    }
}