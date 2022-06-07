#include "Header.h"

Dealer::Dealer()
{
	this->top = 0;
	Card deck[52];

	// create deck of cards for creating shoe
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
			this->shoe[52 * (i)+j] = deck[j];
		}
	}

	this->shuffleShoe();
}

// take top card from shoe
Card Dealer::deal()
{
	this->top++;
	return this->shoe[this->top-1];
}

void Dealer::shuffleShoe()
{
	unsigned seed = std::chrono::system_clock::now().time_since_epoch().count();
	std::shuffle(std::begin(this->shoe), std::end(this->shoe), std::default_random_engine(seed));
}

bool Dealer::playRound(Player& player, Dealer& dealer)
{
	// deals first two cards each
	player.hit(dealer.deal());
	dealer.hit(dealer.deal());
	player.hit(dealer.deal());
	dealer.hit(dealer.deal());

	if (Hand::isBlackjack(dealer.getHand()) && !Hand::isBlackjack(player.getHand()))
	{
		std::cout << "Dealer's cards: ";
		dealer.getHand().show();
		std::cout << " (Blackjack)\n\n";

		std::cout << "Your cards: ";
		player.getHand().show();
		std::cout << " (" + std::to_string(player.getHand().getScore()) + ")\n\n";

		std::cout << "Dealer has blackjack! You lose.";
	}
	else if (!Hand::isBlackjack(dealer.getHand()) && Hand::isBlackjack(player.getHand()))
	{
		std::cout << "Dealer's cards: ";
		dealer.getHand().show();
		std::cout << " (" + std::to_string(dealer.getHand().getScore()) + ")\n\n";

		std::cout << "Your cards: ";
		player.getHand().show();
		std::cout << " (Blackjack)\n\n";

		std::cout << "You have blackjack! You win.";
	}
	else if (Hand::isBlackjack(dealer.getHand()) && Hand::isBlackjack(player.getHand()))
	{
		std::cout << "Dealer's cards: ";
		dealer.getHand().show();
		std::cout << " (Blackjack)\n\n";

		std::cout << "Your cards: ";
		player.getHand().show();
		std::cout << " (Blackjack)\n\n";

		std::cout << "You both have blackjack! Push.";
	}
	else
	{
		char choice;
		bool again = true;

		while (again)
		{
			std::cout << "Dealer's cards: ";
			Card::print(dealer.getHand().getCard(0));
			std::cout << " XX\n";

			std::cout << "Your cards: ";
			player.getHand().show();
			std::cout << " (" << player.getHand().getScore() << ")\n\n";

			std::cout << "What would you like to do?\n";
			std::cout << "H - Hit\n";
			std::cout << "S - Stand\n";
			std::cout << "D - Double down\n\n";

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
				std::cout << " (" + std::to_string(player.getHand().getScore()) + ")\n\n";

				again = false;
				break;

			case 'D': // double down
				player.hit(dealer.deal());

				std::cout << "\nDoubling down...\n\nYour hand: ";
				player.getHand().show();
				std::cout << " (" + std::to_string(player.getHand().getScore()) + ")\n\n";

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
					std::cout << " (" << player.getHand().getScore() << ")\n\n";
				}

				std::cout << "Bust! You lose.\n";
				again = false;
			}
		}

		// dealer's cards don't matter if player busts
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
			std::cout << " (" + std::to_string(dealer.getHand().getScore()) + ")\n\n";

			if (dealer.getHand().getScore() > 21)
			{
				std::cout << "Dealer busts! You win!";
			}
			else if (dealer.getHand().getScore() > player.getHand().getScore())
			{
				std::cout << "You lose!";
			}
			else if (dealer.getHand().getScore() < player.getHand().getScore())
			{
				std::cout << "You win!";
			}
			else
			{
				std::cout << "Push!";
			}
		}
	}

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