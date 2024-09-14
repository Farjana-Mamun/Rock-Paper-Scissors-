HMAC-Based Rock-Paper-Scissors Game:

This project implements a command-line-based Rock-Paper-Scissors game (or any other custom moves set), secured with HMAC (Hash-based Message Authentication Code) to ensure fairness. The computer generates its move at the start of each round and provides the HMAC of the move, which is revealed after the user plays, ensuring the computer does not change its move after seeing the player's choice.


Features:

Secure HMAC Generation: The computer’s move is hashed using HMACSHA256, and the key is revealed after the game to verify the computer's move.
Customizable Moves: You can input any odd number of distinct moves (≥ 3) as command-line arguments, which will be used for the game.
Help Table: The ? option generates a help table that shows the outcome of all possible matchups for the chosen moves.
Game Logic: Determines the winner using an algorithm based on the positions of moves in the array.


How to Play:

1. Compile and run the program with an odd number of distinct moves (≥ 3).
                "dotnet run Rock Paper Scissors", "dotnet run Rock Paper Scissors Lizard Spock"
You can replace "Rock Paper Scissors" with any custom odd-numbered set of moves.
The game will display a menu with the available moves and their indices. The player can:

2. Choose a move by entering the corresponding number.
Enter ? to display the help table.
Enter 0 to exit the game.
You can display the help table by entering ? during the game. This table shows the outcome of each possible player vs. computer move combination.
After the player makes a move, the computer's move, the result of the game, and the HMAC key will be revealed.


Video Demo:

Watch the full video demonstration of this project on YouTube: https://youtu.be/oByAKY8Hagw
