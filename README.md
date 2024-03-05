# Z2J-104 Checkers Application
![grafik](https://github.com/pajus1337/Z2J-104_Checkers/assets/74528115/b481dbe9-8506-4b30-bfec-2e0588d8da36)

## Overview

The application is a checkers game developed in C# for the .NET framework, featuring a user-vs-CPU gameplay on a standard 8x8 checkers board. This application showcases interactive gameplay mechanics and a secondary application acting as a real-time game status window via named pipes.

## Key Components

### GameManager

Manages the overall game flow, initializing the game state and handling the progression of gameplay.

### Board

Represents the game board, storing the state and contents of each square.

### Pawns (PlayerPawn and CpuPawn)

Abstract base class `Pawn` with derived classes for player and CPU pawns, incorporating symbols and end-of-board logic specific to each.

### PawnController

Oversees pawn placement and movement on the board.

### GameStateController

Monitors and adjusts the game state, including turn changes and win/loss conditions.

### MovementAnalyzer

Evaluates potential pawn movements to ensure they adhere to the rules of checkers.

### CPUChoiceAnalyzer

Implements decision logic for CPU pawn movements.

### GameStatusSender

Facilitates communication, sending game statuses and messages to the user.

## Secondary Application: Game Status Window

A companion application, `Z2J_CheckersGameStatus`, operates alongside the main game to display real-time status updates and messages through a named pipe server ("GameStatusPipe"). This enhances the gameplay experience with live feedback.

## Running the Application

Both applications require a .NET capable environment. Dependencies are configured via `DependencyInjectionConfig` using `Microsoft.Extensions.DependencyInjection`.

### To run:
- Ensure both the main game and the status window applications are executed concurrently for the full experience.
