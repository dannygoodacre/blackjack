cmake_minimum_required(VERSION 3.16)

project(blackjack 
    VERSION 1.0.0 
    DESCRIPTION "Blackjack"
)

enable_testing()
add_subdirectory(model/tests)

add_library(blackjackengine STATIC "")
add_subdirectory(model/src)