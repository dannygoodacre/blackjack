cmake_minimum_required(VERSION 3.16)

project(blackjack 
    VERSION 1.0.0 
    DESCRIPTION "Blackjack"
)

enable_testing()
add_subdirectory(engine/tests)

add_library(blackjackengine STATIC "")
add_subdirectory(engine/src)

add_executable(blackjackcli "")
add_subdirectory(cli)
target_link_libraries(blackjackcli blackjackengine)