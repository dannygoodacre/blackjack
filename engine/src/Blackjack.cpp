#include "../include/Blackjack.h"
#include "BlackjackData.h"

BlackjackData *data;

std::vector<std::string> getHandAsVectorString(Player p)
{
    std::vector<std::string> hand;
    for (int i = 0; i < p.getHand().getNumberOfCards(); i++)
    {
        Card card = p.getHand().getCardAt(i);
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
    data->player.resetHand();
    data->dealer.resetHand();
    data->isInProgress = true;
    data->currentBet = bet;
    data->player.setWallet(data->player.getWallet() - bet);

    data->possibleMoves = {'H', 'S'};
    if (data->player.getWallet() >= data->currentBet)
        data->possibleMoves.push_back('D');

    data->player.addToHand(data->shoe.drawCard());
    data->dealer.addToHand(data->shoe.drawCard());
    data->player.addToHand(data->shoe.drawCard());
    data->dealer.addToHand(data->shoe.drawCard());

    bool isPlayerBlackjack = data->player.getHandScore() == 21;
    bool isDealerBlackjack = data->dealer.getHandScore() == 21;

    if (!isPlayerBlackjack && !isDealerBlackjack)
        return;

    data->isInProgress = false;
    data->possibleMoves.clear();

    if (isPlayerBlackjack)
    {
        // TODO: Calculating outcome and crediting should be its own function.
        if (isDealerBlackjack)
        {
            data->outcome = 'D';
            data->player.setWallet(data->player.getWallet() + data->currentBet);
            data->numDraws++;
        }
        else
        {
            data->outcome = 'W';
            data->player.setWallet(data->player.getWallet() + 2.5*data->currentBet);
            data->numWins++;
        }
    }
    else if (isDealerBlackjack)
    {
        data->outcome = 'L';
        data->numLosses++;
    }
}

void Blackjack::hit()
{
    data->player.addToHand(data->shoe.drawCard());
    data->possibleMoves = {'H', 'S'};

    if (data->player.getHandScore() >= 21)
        Blackjack::stand();
}

void Blackjack::stand()
{
    data->isInProgress = false;
    data->possibleMoves.clear();

    if (data->player.getHandScore() > 21)
    {
        data->outcome = 'L';
        data->numLosses++;
        return;
    }

    while (data->dealer.getHandScore() < 17)
        data->dealer.addToHand(data->shoe.drawCard());
    if (data->dealer.getHandScore() == 17 && data->dealer.getHand().isSoft())
        data->dealer.addToHand(data->shoe.drawCard());

    int playerScore = data->player.getHandScore();
    int dealerScore = data->dealer.getHandScore();

    if (dealerScore > 21 || dealerScore < playerScore) // Win.
    {
        data->outcome = 'W';
        data->player.setWallet(data->player.getWallet() + 2*data->currentBet);
        data->numWins++;
    }
    else if (dealerScore > playerScore)// Loss.
    {
        data->outcome = 'L';
        data->numLosses++;
    }
    else // Push.
    {
        data->outcome = 'D';
        data->player.setWallet(data->player.getWallet() + data->currentBet);
        data->numDraws++;
    }
}

void Blackjack::doubleDown()
{
    data->isInProgress = false;
    data->player.setWallet(data->player.getWallet() - data->currentBet);
    data->currentBet *= 2;
    hit();
    stand();
}

std::vector<std::string> Blackjack::getPlayerHand()
{
    return getHandAsVectorString(data->player);
}

std::vector<std::string> Blackjack::getDealerHand()
{
    return getHandAsVectorString(data->dealer);
}

int Blackjack::getPlayerHandScore()
{
    return data->player.getHandScore();
}

int Blackjack::getDealerHandScore()
{
    return data->dealer.getHandScore();
}

bool Blackjack::getIsRoundInProgress()
{
    return data->isInProgress;
}

std::vector<char> Blackjack::getPossibleMoves()
{
    if (!getIsRoundInProgress())
        return {};
    return data->possibleMoves;
}

char Blackjack::getOutcome()
{
    if (getIsRoundInProgress())
        return 0;
    return data->outcome;
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