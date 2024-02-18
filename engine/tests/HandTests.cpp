#include "../src/Card.h"
#include "../src/Hand.h"

#include <gtest/gtest.h>
#include <tuple>

class HandIsBlackjackTestFixture
: public testing::TestWithParam<std::tuple<Hand, bool>>
{};

TEST_P(HandIsBlackjackTestFixture, isBlackjack)
{
    Hand hand = std::get<0>(GetParam());
    bool expect = std::get<1>(GetParam());
    ASSERT_EQ(hand.isBlackjack(), expect);
}

INSTANTIATE_TEST_SUITE_P
(
    HandIsBlackjackTests,
    HandIsBlackjackTestFixture,
    testing::Values
    (
        std::make_tuple(Hand({Card("A", "d"), Card("K", "c")}), true)
    )
);

class HandIsBustTestFixture
: public testing::TestWithParam<std::tuple<Hand, bool>>
{};

TEST_P(HandIsBustTestFixture, isBust)
{
    Hand hand = std::get<0>(GetParam());
    bool expect = std::get<1>(GetParam());
    ASSERT_EQ(hand.isBust(), expect);
}

INSTANTIATE_TEST_SUITE_P
(
    HandIsBustTests,
    HandIsBustTestFixture,
    testing::Values
    (
        std::make_tuple(Hand({Card("5", "d"), Card("10", "c")}), false),
        std::make_tuple(Hand({Card("10", "c"), Card("K", "d"), Card("4", "c")}), true),
        std::make_tuple(Hand({Card("A", "s"), Card("A", "h")}), false)
    )
);

class HandIsSoftTestFixture
: public testing::TestWithParam<std::tuple<Hand, bool>>
{};

TEST_P(HandIsSoftTestFixture, isSoft)
{
    Hand hand = std::get<0>(GetParam());
    bool expect = std::get<1>(GetParam());
    ASSERT_EQ(hand.isSoft(), expect);
}

INSTANTIATE_TEST_SUITE_P
(
    HandIsSoftTests,
    HandIsSoftTestFixture,
    testing::Values
    (
        std::make_tuple(Hand({Card("7", "d"), Card("8", "h")}), false),
        std::make_tuple(Hand({Card("Q", "h"), Card("A", "d")}), true),
        std::make_tuple(Hand({Card("K", "s"), Card("A", "h"), Card("3", "s")}), false)
    )
);

class HandSplittingTestFixture
: public testing::TestWithParam<std::tuple<Hand, Hand>>
{};

TEST_P(HandSplittingTestFixture, split)
{
    Hand hand = std::get<0>(GetParam());
    Hand exSplitHand = std::get<1>(GetParam());

    Hand splitHand = hand.split();
    ASSERT_EQ(exSplitHand.getCardAt(0), splitHand.getCardAt(0));
}

INSTANTIATE_TEST_SUITE_P
(
    HandSplittingTests,
    HandSplittingTestFixture,
    testing::Values
    (
        std::make_tuple(Hand({Card("A", "c"), Card("A", "d")}), Hand({Card("A", "d")}))
    )
);