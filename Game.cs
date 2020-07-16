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
            TurnResult player1Result;
            TurnResult player2Result;
            int winningScore = 5000;
            bool player1Turn = true;
            int startDice = 6;
            int player1Score = 0;
            int player2Score = 0;
            while(player1Score <= winningScore && player2Score <= winningScore){
                if(player1Turn) {
                    Console.WriteLine("Player 1 your turn!");
                    player1Result = playTurn(startDice, player1Score);
                    player1Score += player1Result.score;
                    player1Turn = false;
                } else {
                    Console.WriteLine("Player 2 your turn!");
                    player2Result = playTurn(startDice, player2Score);
                    player2Score += player2Result.score;
                    player1Turn = true;
                }
            }
            if(player1Score > player2Score) {
                Console.WriteLine("Player 1 wins with a score of {0}", player1Score);
            } else {
                Console.WriteLine("Player 2 wins with a score of {0}", player2Score);
            }
        }

        public ConsoleFarkle.TurnResult playTurn(int startDice, int playerScore) {
            if(startDice == 0) { // TODO : if dice = 0 restart. fix this later
                TurnResult result = new TurnResult(playerScore, true);
                return result;
            }
            int currentScore = playerScore;
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
                TurnResult result2 = new TurnResult(playerScore, true);
                return result2;
            }
            Console.WriteLine("Enter numbers of what options you want to take.");
            Console.WriteLine("_________");
            string input = Console.ReadLine();
            List<RollResult> rollResultsSelected = farkle.determineRollSelections(input, rollResults);
            
            currentScore = farkle.calculateScore(rollResultsSelected, currentScore);
            int diceForNextRoll = farkle.calculateRemainingDice(rollResultsSelected, diceCount);
            Console.WriteLine("Here is the remaining dice count {0}, Current Score: {1}", diceForNextRoll, currentScore);
            playTurn(diceForNextRoll, currentScore);
            TurnResult result3 = new TurnResult(playerScore, true);
            return result3;
        }
        public void computerGame() {
            Console.WriteLine("Play against computer");
        }
    }
}

