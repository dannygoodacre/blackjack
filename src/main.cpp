#include "Header.h"

int main()
{
	Player player;
	Dealer dealer;

	bool again = true;
	while (again)
	{
		again = Dealer::playRound(player, dealer);
	}

	std::cout << "\nGoodbye!\n";
}