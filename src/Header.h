#pragma once

#include <string>
#include <cstring>
#include <iostream>
#include <algorithm>
#include <array>
#include <chrono>
#include <random>

class Card
{
public:
	static const std::string RANKS[13];
	static const char SUITS[4];

	static bool isNumber(const std::string& s);
	static int scoreOf(Card card);
	static void print(Card card);

	Card();
	Card(std::string rank, char suit);
	std::string getRank();
	char getSuit();

private:
	std::string rank;
	char suit;
};


class Hand
{
public:
	static bool isBlackjack(Hand hand);

	Hand();
	void addCard(Card card);
	int getScore();
	bool isBust();
	void show();
	void reset();
	Card getCard(int n);

private:
	std::array<Card, 14> cards;
	int numberOfAces;
	int score;
	int numberOfCards; // first available index in cards
};


class Player
{
public:
	Player(int startingWallet);
	void hit(Card card);
	Hand getHand();
	void resetHand();
	int getWallet();
	void addToWallet(int val);

private:
	Hand hand;
	int wallet;
};


class Dealer: public Player
{
public:
	Dealer(int startingWallet);
	static bool playRound(Player& player, Dealer& dealer);
	static int takeBet(Player& player);

private:
	Card shoe[416];
	int top; // position of top card in shoe

	Card deal();
	void shuffleShoe();
};