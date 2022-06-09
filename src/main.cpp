#include "Header.h"

int main()
{
	Player player(100);
	Dealer dealer(0);

	bool again = true;
	while (again)
	{
		again = Dealer::playRound(player, dealer);
	}

	std::cout << "\nGoodbye!\n";
}