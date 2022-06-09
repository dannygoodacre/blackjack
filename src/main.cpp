#include "Header.h"

int main()
{
	Player player(100);
	Dealer dealer(0);

	std::cout << "Remember: when the fun stops, stop.\n\n";

	bool again = true;
	while (again)
	{
		again = Dealer::playRound(player, dealer);
	}

	std::cout << "\nGoodbye!\n";
}

// TODO: Prevent doubling down with insufficient funds