#include "Header.h"

Dealer::Dealer(int startingWallet) : Player(startingWallet)
{
	this->top = 0;
	Card deck[52];

	// create deck of cards
	int k = 0;
	for (int i = 0; i < 4; i++)
	{
		for (int j = 0; j < 13; j++)
		{
			deck[k] = Card(Card::RANKS[j], Card::SUITS[i]);
			k++;
		}
	}

	// create shoe of cards
	for (int i = 0; i < 8; i++)
	{
		for (int j = 0; j < 52; j++)
		{
			this->shoe[52*i + j] = deck[j];
		}
	}

	this->shuffleShoe();
}

Card Dealer::deal()
{
	this->top++;
	return this->shoe[this->top-1];
}

void Dealer::shuffleShoe()
{
	unsigned seed
	= std::chrono::system_clock::now().time_since_epoch().count();
	std::shuffle(std::begin(this->shoe),
		std::end(this->shoe), std::default_random_engine(seed));
}

int Dealer::getBet(Player& player)
{
	int bet;
	bool valid = false;
	while (!valid)
	{
		std::cout << "Bet (" + std::to_string(player.getWallet()) + "): ";
		std::cin >> bet;
		std::cout << "\n";
		if (bet > player.getWallet())
		{
			std::cout << "Insufficient funds. Please enter a lower amount.";
		}
		else
		{
			valid = true;
		}
	}

	player.addToWallet(-bet);
	return bet;
}

bool Dealer::playRound(Player& player, Dealer& dealer)
{
	int bet = dealer.getBet(player);

	player.hit(dealer.deal());
	dealer.hit(dealer.deal());
	player.hit(dealer.deal());
	dealer.hit(dealer.deal());

if (dealer.getHand().isBlackjack() || player.getHand().isBlackjack())
	{
		// someone has blackjack
		std::cout << "Dealer's cards: ";
		dealer.getHand().show();
		dealer.getHand().showScore();

		std::cout << "Your cards: ";
		player.getHand().show();
		player.getHand().showScore();

		if (dealer.getHand().isBlackjack()
		&& !player.getHand().isBlackjack())
		{
			std::cout << "Dealer has blackjack! You lose.";
		}
		else if (!dealer.getHand().isBlackjack()
		&& player.getHand().isBlackjack())
		{
			std::cout << "You have blackjack! You win.";
			player.addToWallet(2.5 * bet);
		}
		else if (dealer.getHand().isBlackjack()
		&& player.getHand().isBlackjack())
		{
			std::cout << "You both have blackjack! Push.";
			player.addToWallet(bet);
		}
	}
	else
	{
		// neither have blackjack
		char choice;
		int round = 1;
		bool doubleDown = false;

		bool again = true;
		while (again)
		{
			std::cout << "Dealer's cards: ";
			Card::print(dealer.getHand().getCard(0));
			std::cout << " XX\n";

			std::cout << "Your cards: ";
			player.getHand().show();
			player.getHand().showScore();

			std::cout << "What would you like to do?\n";
			std::cout << "H - Hit\n";
			std::cout << "S - Stand\n";
			std::cout << ((round == 1) ? "D - Double down\n\n" : "");
			round++;

			std::cout << "choice: ";
			std::cin >> choice;

			switch (toupper(choice))
			{
			case 'H': // hit
				std::cout << "\nHitting...\n\n";
				player.hit(dealer.deal());

				again = true;
				break;

			case 'S': // stand
				std::cout << "\nStanding...\n\nYour hand: ";
				player.getHand().show();
				player.getHand().showScore();

				again = false;
				break;

			case 'D': // double down
				player.hit(dealer.deal());
				player.addToWallet(-bet);
				doubleDown = true;

				std::cout << "\nDoubling down...\n\nYour hand: ";
				player.getHand().show();
				player.getHand().showScore();

				again = false;
				break;

			default:
				std::cout << "\nInvalid choice. Try again.\n\n";

				again = true;
			}

			if (player.getHand().isBust())
			{
				if (again)
				{
					std::cout << "Your cards: ";
					player.getHand().show();
					player.getHand().showScore();
				}

				std::cout << "Bust! You lose.";
				again = false;
			}
		}

		if (!player.getHand().isBust())
		{
			if (dealer.getHand().getScore() < 17)
			{
				std::cout << "Dealer must draw to 17: ";

				while (dealer.getHand().getScore() < 17)
				{
					dealer.hit(dealer.deal());
				}
			}
			else
			{
				std::cout << "Dealer's hand: ";
			}

			dealer.getHand().show();
			dealer.getHand().showScore();

			if (dealer.getHand().getScore() > 21)
			{
				std::cout << "Dealer busts! You win!";
				player.addToWallet(((doubleDown) ? 4:2) * bet);
			}
			else if (dealer.getHand().getScore()
			> player.getHand().getScore())
			{
				std::cout << "You lose!";
			}
			else if (dealer.getHand().getScore()
			< player.getHand().getScore())
			{
				std::cout << "You win!";
				player.addToWallet(((doubleDown) ? 4:2) * bet);
			}
			else
			{
				std::cout << "Push!";
				player.addToWallet(((doubleDown) ? 2:1) * bet);
			}
		}
	}

	if (player.getWallet() <= 0)
	{
		std::cout << "\n\nYour wallet is empty.";
		return false;
	}
	else
	{
		char choice;
		std::cout << "\n\nWould you like to play again? (Y/N): ";
		std::cin >> choice;

		switch (toupper(choice))
		{
		case 'Y':
			player.resetHand();
			dealer.resetHand();
			return true;
		case 'N':
			return false;
		default:
			std::cout << "Invalid choice, exiting";
			return false;
		}
	}
}