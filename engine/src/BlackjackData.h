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
    int outcome;
    int currentBet;
    std::vector<char> possibleMoves;
};