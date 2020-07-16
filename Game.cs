using System;
using System.Collections.Generic;  

namespace ConsoleFarkle
{
    class Game
    {
        public void startGame()
        {
            Console.WriteLine("Welcome to Console Farkle. \n Press 1 To play against another player \n Press 2 To play against computer.");
            int gameMode = Int32.Parse(Console.ReadLine());
            switch(gameMode) {
                case 1 :
                    Console.WriteLine("Play against another player");
                    twoPlayerGame();
                    break;
                case 2 : 
                    computerGame();
                    break;
                default : 
                    Console.WriteLine("You didn't enter a valid option");
                    break;
            }
        }

        public void twoPlayerGame() {
            int startDice = 6;
            int player1Score = 0;
            int player2Score = 0;
            playTurn(startDice, player1Score, player2Score, true);
        }

        public void playTurn(int startDice, int player1Score, int player2Score, bool isPlayer1) {
            int currentScore;
            if(isPlayer1) {
                Console.WriteLine("Player 1 your turn!"); 
                currentScore = player1Score;
            } else {
                Console.WriteLine("Player 2 your turn!");
                currentScore = player2Score;
            }
            if(currentScore >= 5000) { // rethink game state logic. at the bottom for this if statement
                Console.WriteLine("you win!");
                return;
            }
            Farkle farkle = new Farkle();
            int diceCount = startDice;
            int[] rollTest = farkle.roll(diceCount);
            List<RollResult> rollResults = farkle.returnOptions(rollTest);
            Console.WriteLine("Roll test, Current Score: {0}", currentScore);
            foreach(int roll in rollTest) {
                Console.WriteLine(roll);
            }
            Console.WriteLine("_________");
            for(int i = 0; i < rollResults.Count; i++) {
                Console.WriteLine((i + 1) + ". " + rollResults[i]);
            }
            if(rollResults.Count == 1 && rollResults[0] == RollResult.Nothing) {
                Console.WriteLine("FARKLE!!");
                return;
            }
            Console.WriteLine("Enter numbers of what options you want to take.");
            Console.WriteLine("_________");
            string input = Console.ReadLine();
            List<RollResult> rollResultsSelected = farkle.determineRollSelections(input, rollResults);
            currentScore = farkle.calculateScore(rollResultsSelected, currentScore);
            int diceForNextRoll = farkle.calculateRemainingDice(rollResultsSelected, diceCount);
            Console.WriteLine("Here is the remaining dice count {0}, Current Score: {1}", diceForNextRoll, currentScore);
            if(isPlayer1) {
                Console.WriteLine("Player 1 your turn!"); 
                playTurn(diceForNextRoll, currentScore, player2Score, !isPlayer1);
            } else {
                Console.WriteLine("Player 2 your turn!");
                playTurn(diceForNextRoll, player1Score, currentScore, isPlayer1);
            }
        }
        public void computerGame() {
            Console.WriteLine("Play against computer");
        }
    }
}

