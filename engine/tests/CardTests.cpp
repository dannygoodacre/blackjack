#include "../src/Card.h"

#include <gtest/gtest.h>
#include <string>
#include <tuple>

class CardIsAceTestFixture
: public testing::TestWithParam<std::tuple<Card, bool>> 
{};

TEST_P(CardIsAceTestFixture, isAce)
{
    Card card = std::get<0>(GetParam());
    bool expect = std::get<1>(GetParam());
    ASSERT_EQ(card.isAce(), expect);
}

INSTANTIATE_TEST_SUITE_P
(
    CardIsAceTests,
    CardIsAceTestFixture,
    testing::Values
    (
        std::make_tuple(Card("A", "s"), true), 
        std::make_tuple(Card("J", "c"), false)
    )
);

class CardGetScoreTestFixture
: public testing::TestWithParam<std::tuple<Card, int>>
{};

TEST_P(CardGetScoreTestFixture, getScore)
{
    Card card = std::get<0>(GetParam());
    int expect = std::get<1>(GetParam());
    ASSERT_EQ(card.getScore(), expect);
}

INSTANTIATE_TEST_SUITE_P
(
    CardGetScoreTests,
    CardGetScoreTestFixture,
    testing::Values
    (
        std::make_tuple(Card("A", "s"), 11),
        std::make_tuple(Card("Q", "d"), 10),
        std::make_tuple(Card("4", "h"), 4)
    )
);