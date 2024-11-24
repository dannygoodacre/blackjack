cmake_minimum_required(VERSION 3.16)

project(blackjack 
    VERSION 1.0.0 
    DESCRIPTION "Blackjack"
)

enable_testing()
add_subdirectory(engine/tests)

add_library(blackjackengine STATIC "")
add_subdirectory(engine/src)

set_target_properties(blackjackengine PROPERTIES POSITION_INDEPENDENT_CODE ON)

add_executable(blackjackcli "")
add_subdirectory(cli)
target_link_libraries(blackjackcli blackjackengine)

add_library(blackjackenginewrapper SHARED
    engine/src/BlackjackWrapper.cpp
)

target_include_directories(blackjackenginewrapper PUBLIC
    ${CMAKE_CURRENT_SOURCE_DIR}/engine/include
)

target_link_libraries(blackjackenginewrapper blackjackengine)