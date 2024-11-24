#include "BlackjackWrapper.h"
#include "Blackjack.h"

#include <cstring>
#include <algorithm>
#include <iostream>

/**
 * Helper function to copy a vector strings to C-style arrays.
 * @param sourceHand The vector of strings to copy.
 */
int copyHand(const std::vector<std::string>& sourceHand, char** hand, int maxCards)
{
    int count = std::min(static_cast<int>(sourceHand.size()), maxCards);
    for (int i = 0; i < count; ++i)
    {
        std::strncpy(hand[i], sourceHand[i].c_str(), 3);
        hand[i][3] = '\0'; // Null-termination.
    }

    return count;
}

void setupGame(int initialPlayerWallet)
{
    Blackjack::setupGame(initialPlayerWallet);
}

void startRound(int bet)
{
    Blackjack::startRound(bet);
}

void hit(int n)
{
    Blackjack::hit(n);
}

void stand(int n)
{
    Blackjack::stand(n);
}

void doubleDown(int n)
{
    Blackjack::doubleDown(n);
}

void split(int n)
{
    Blackjack::split(n);
}

void endRound()
{
    Blackjack::endRound();
}

int getPlayerHand(int n, char** hand, int maxCards)
{
    std::vector<std::string> cards = Blackjack::getPlayerHand(n);
    return copyHand(cards, hand, maxCards);
}

int getDealerHand(char** hand, int maxCards)
{
    std::vector<std::string> cards = Blackjack::getDealerHand();
    return copyHand(cards, hand, maxCards);
}

void getPossibleMoves(int n, char* moves, int maxMoves)
{
    std::vector<char> possibleMoves = Blackjack::getPossibleMoves(n);
    int count = std::min(static_cast<int>(possibleMoves.size()), maxMoves);
    for (int i = 0; i < count; ++i)
        moves[i] = possibleMoves[i];
}

char getOutcome(int n)
{
    return Blackjack::getOutcome(n);
}

int getPlayerWallet()
{
    return Blackjack::getPlayerWallet();
}

int getNumberOfHands()
{
    return Blackjack::getNumberOfHands();
}

int getHandsInPlay(int* hands, int maxHands)
{
    std::vector<int> handsInPlay = Blackjack::getHandsInPlay();
    int count = std::min(static_cast<int>(handsInPlay.size()), maxHands);

    for (int i = 0; i < count; i++)
        hands[i] = handsInPlay[i];

    return count;
}

int getPlayerHandScore(int n)
{
    return Blackjack::getPlayerHandScore(n);
}

int getDealerHandScore()
{
    return Blackjack::getDealerHandScore();
}

int getIsRoundInProgress()
{
    return Blackjack::getIsRoundInProgress() ? 1 : 0;
}

int getNumberOfWins()
{
    return Blackjack::getNumberOfWins();
}

int getNumberOfLosses()
{
    return Blackjack::getNumberOfLosses();
}

int getNumberOfDraws()
{
    return Blackjack::getNumberOfDraws();
}