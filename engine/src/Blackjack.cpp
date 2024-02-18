#include "../include/Blackjack.h"
#include "BlackjackData.h"

BlackjackData *data;

std::vector<std::string> getHandAsVectorString(Player p, int n)
{
    std::vector<std::string> hand;
    for (int i = 0; i < p.getHand(n).getNumberOfCards(); i++)
    {
        Card card = p.getHand(n).getCardAt(i);
        hand.push_back(card.getRank() + card.getSuit());
    }

    return hand;
}

void Blackjack::setupGame(int initialPlayerWallet)
{
    delete data;
    data = new BlackjackData();

    data->player.setWallet(initialPlayerWallet);
    data->numWins = 0;
    data->numLosses = 0;
    data->numDraws = 0;
}

void Blackjack::startRound(int bet)
{
    data->player.resetHands();
    data->dealer.resetHands();
    data->isInProgress = true;
    data->currentBet[0] = bet;
    data->player.setWallet(data->player.getWallet() - bet);

    data->possibleMoves[0] = {'H', 'S'};
    if (data->player.getWallet() >= data->currentBet[0])
        data->possibleMoves[0].push_back('D');

    data->player.addToHand(0, data->shoe.drawCard());
    data->dealer.addToHand(0, data->shoe.drawCard());
    data->player.addToHand(0, data->shoe.drawCard());
    data->dealer.addToHand(0, data->shoe.drawCard());

    bool isPlayerBlackjack = data->player.getHandScore(0) == 21;
    bool isDealerBlackjack = data->dealer.getHandScore(0) == 21;

    if (!isPlayerBlackjack && !isDealerBlackjack)
        return;

    data->isInProgress = false;
    data->possibleMoves[0].clear();

    if (isPlayerBlackjack)
    {
        // TODO: Calculating outcome and crediting should be its own function.
        if (isDealerBlackjack)
        {
            data->outcome[0] = 'D';
            data->player.setWallet(data->player.getWallet() + data->currentBet[0]);
            data->numDraws++;
        }
        else
        {
            data->outcome[0] = 'W';
            data->player.setWallet(data->player.getWallet() + 2.5*data->currentBet[0]);
            data->numWins++;
        }
    }
    else if (isDealerBlackjack)
    {
        data->outcome[0] = 'L';
        data->numLosses++;
    }
}

void Blackjack::hit(int n)
{
    data->player.addToHand(n, data->shoe.drawCard());
    data->possibleMoves[n] = {'H', 'S'};

    if (data->player.getHandScore(n) >= 21)
        Blackjack::stand(n);
}

void Blackjack::stand(int n)
{
    data->isInProgress = false;
    data->possibleMoves[n].clear();

    if (data->player.getHandScore(n) > 21)
    {
        data->outcome[n] = 'L';
        data->numLosses++;
        return;
    }

    while (data->dealer.getHandScore(n) < 17)
        data->dealer.addToHand(n, data->shoe.drawCard());
    if (data->dealer.getHandScore(n) == 17 && data->dealer.getHand(n).isSoft())
        data->dealer.addToHand(n, data->shoe.drawCard());

    int playerScore = data->player.getHandScore(n);
    int dealerScore = data->dealer.getHandScore(n);

    if (dealerScore > 21 || dealerScore < playerScore) // Win.
    {
        data->outcome[n] = 'W';
        data->player.setWallet(data->player.getWallet() + 2*data->currentBet[n]);
        data->numWins++;
    }
    else if (dealerScore > playerScore)// Loss.
    {
        data->outcome[n] = 'L';
        data->numLosses++;
    }
    else // Push.
    {
        data->outcome[n] = 'D';
        data->player.setWallet(data->player.getWallet() + data->currentBet[n]);
        data->numDraws++;
    }
}

void Blackjack::doubleDown(int n)
{
    data->isInProgress = false;
    data->player.setWallet(data->player.getWallet() - data->currentBet[n]);
    data->currentBet[n] *= 2;
    hit(n);
    stand(n);
}

std::vector<std::string> Blackjack::getPlayerHand(int n)
{
    return getHandAsVectorString(data->player, n);
}

std::vector<std::string> Blackjack::getDealerHand()
{
    return getHandAsVectorString(data->dealer, 0);
}

int Blackjack::getPlayerHandScore(int n)
{
    return data->player.getHandScore(n);
}

int Blackjack::getDealerHandScore()
{
    return data->dealer.getHandScore(0);
}

bool Blackjack::getIsRoundInProgress()
{
    return data->isInProgress;
}

std::vector<char> Blackjack::getPossibleMoves(int n)
{
    if (!getIsRoundInProgress())
        return {};
    return data->possibleMoves[n];
}

char Blackjack::getOutcome(int n)
{
    if (getIsRoundInProgress())
        return 0;
    return data->outcome[n];
}

int Blackjack::getPlayerWallet()
{
    return data->player.getWallet();
}

int Blackjack::getNumberOfWins()
{
    return data->numWins;
}

int Blackjack::getNumberOfLosses()
{
    return data->numLosses;
}

int Blackjack::getNumberOfDraws()
{
    return data->numDraws;
}