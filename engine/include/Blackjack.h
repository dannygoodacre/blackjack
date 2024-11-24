#include <string>
#include <vector>

namespace Blackjack
{
    /**
     * Set up the game.
     * @param initialPlayerWallet The initial amount of money the player has.
     */
    void setupGame(int initialPlayerWallet);

    /**
     * Start a new round.
     * @param bet The amount of money to bet.
     */
    void startRound(int bet);

    /**
     * Draw a card.
     * @param n The index of the hand.
     */
    void hit(int n);

    /**
     * Draw no more cards and end the round.
     * @param n The index of the hand.
     */
    void stand(int n);
    
    /**
     * Draw one more card and double the bet, and end the round.
     * @param n The index of the hand.
     */
    void doubleDown(int n);

    /**
     * Split a hand into two hands, drawing a new card for each hand and 
     * betting the same amount as the original bet on each.
     * @param n The index of the hand to split.
     */
    void split(int n);

    /**
     * End the current round.
     */
    void endRound();

    /**
     * Get the cards in the player's hand.
     * @param n The index of the hand.
     * @return A vector of strings representing the cards in the hand.
     */
    std::vector<std::string> getPlayerHand(int n);

    /**
     * Get the cards in the dealer's hand.
     * @return A vector of strings representing the cards in the hand.
     */
    std::vector<std::string> getDealerHand();

    /**
     * Get the possible moves for a hand.
     * @param n The index of the hand.
     * @return A vector of possible moves or an empty vector if round not in 
     * progress.
     */
    std::vector<char> getPossibleMoves(int n);

    /**
     * Get the outcome of the round.
     * @param n The index of the hand.
     * @return The outcome of the round or 0 if in progress.
     */
    char getOutcome(int n);

    /**
     * Get the amount of money the player has.
     * @return The amount of money the player has.
     */
    int getPlayerWallet();

    /**
     * Get the number of hands in play.
     * @return The number of hands in play.
     */
    int getNumberOfHands();

    /**
     * Get the hands in play.
     * @return A vector of indices of the hands in play.
     */
    std::vector<int> getHandsInPlay();

    /**
     * Get the score of a player's hand.
     * @param n The index of the hand.
     * @return The score of the hand.
     */
    int getPlayerHandScore(int n);

    /**
     * Get the score of the dealer's hand.
     * @return The score of the hand.
     */
    int getDealerHandScore();

    /**
     * Get whether a round is in progress.
     * @return True if a round is in progress, false if not.
     */
    bool getIsRoundInProgress();

    /**
     * Get the number of wins.
     * @return The number of wins.
     */
    int getNumberOfWins();

    /**
     * Get the number of losses.
     * @return The number of losses.
     */
    int getNumberOfLosses();

    /**
     * Get the number of draws.
     * @return The number of draws.
     */
    int getNumberOfDraws();   
}