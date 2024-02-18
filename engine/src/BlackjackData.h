#pragma once

#include "../src/Shoe.h"
#include "../src/Player.h"

#include <vector>

class BlackjackData
{
public:
    Shoe shoe;
    Player player;
    Player dealer;
    bool isInProgress;
    int outcome[4];
    int currentBet[4];
    std::vector<char> possibleMoves[4];
    int numHands;
    int numWins;
    int numLosses;
    int numDraws;
};