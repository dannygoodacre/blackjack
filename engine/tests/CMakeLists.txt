find_package(GTest REQUIRED)

add_executable(AllTests 
    AllTests.cpp
    CardTests.cpp
    HandTests.cpp
)

target_link_libraries(AllTests
    GTest::GTest
    blackjackengine
)

include(GoogleTest)
gtest_discover_tests(AllTests)