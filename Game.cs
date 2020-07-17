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
            TurnResult turnResult;
            int winningScore = 5000;
            bool player1Turn = true;
            int startDice = 6;
            int player1Score = 0;
            int player2Score = 0;
            while(player1Score <= winningScore && player2Score <= winningScore) {
                if(player1Turn) {
                    // TODO: fix this, I am adding score but then when I farkle i add 0, play turn either needs to handle the whole turn which it can.
                    Console.WriteLine("Player 1 your turn!");
                    turnResult = playTurn(startDice, 0);
                    Console.WriteLine("Score: {0}, EndTurn: {1}, Farkle: {2}", turnResult.score, turnResult.endTurn, turnResult.farkle);
                    if(turnResult.endTurn && !turnResult.farkle) {
                        player1Score += turnResult.score;
                        player1Turn = false;
                    } else if(turnResult.farkle) {
                        player1Turn = false;
                    }
                } else {
                    Console.WriteLine("Player 2 your turn!");
                    turnResult = playTurn(startDice, 0);
                    Console.WriteLine("Score: {0}, EndTurn: {1}, Farkle: {2}", turnResult.score, turnResult.endTurn, turnResult.farkle);
                    if(turnResult.endTurn && !turnResult.farkle) {
                        player2Score += turnResult.score;
                        player1Turn = true;
                    } else if(turnResult.farkle) {
                        player1Turn = true;
                    }
                }
            }
        }
        public ConsoleFarkle.TurnResult playTurn(int startDice, int turnScore) {
            TurnResult turnResult;
            Farkle farkle = new Farkle();
            int currentDiceCount = startDice;
            int currentScore = turnScore;
            int[] diceRolls = farkle.roll(currentDiceCount);
            List<RollResult> rollResults = farkle.returnOptions(diceRolls);
            foreach(int roll in diceRolls) {
                Console.Write("{0} | ", roll);
            }
            Console.WriteLine(" ");
            Console.WriteLine("_________________");
            for(int i = 0; i < rollResults.Count; i++) {
                Console.WriteLine("{0}. {1}", i + 1, rollResults[i]);
            }
            if(rollResults[0] == RollResult.Nothing) {
                Console.WriteLine("FARKLE!");
                turnResult = new TurnResult(0, true, true);
                Console.WriteLine("score: {0}, endturn: {1}, farkle: {2}", turnResult.score, turnResult.endTurn, turnResult.farkle);
                return turnResult;
            }
            Console.WriteLine("Enter numbers of what options you want to take. If you want to take the roll, end your input with a /");
            Console.WriteLine("_______________");
            string input = Console.ReadLine();
            List<RollResult> rollResultsSelected = farkle.determineRollSelections(input, rollResults);
            currentScore = farkle.calculateScore(rollResultsSelected, turnScore);
            currentDiceCount = farkle.calculateRemainingDice(rollResultsSelected, currentDiceCount);
            Console.WriteLine("Current Turn Score: {0}, Remaining Dice: {1}", currentScore, currentDiceCount);
            bool endTurnTakeScore = (rollResultsSelected[rollResultsSelected.Count - 1] == RollResult.TakeScore ? true : false);
            Console.WriteLine("{0} endturntakescore",endTurnTakeScore);
            turnResult = new TurnResult(currentScore, endTurnTakeScore, false);
            Console.WriteLine("Score: {0}, Endturn: {1}, Farkle: {2}", turnResult.score, turnResult.endTurn, turnResult.farkle);
            Console.WriteLine("\n\n\n");
            if(!turnResult.endTurn) {
                if(currentDiceCount == 0){
                    currentDiceCount = 6;
                } 
                playTurn(currentDiceCount, currentScore);
            }
            return turnResult;
        }
        public void computerGame() {
            Console.WriteLine("Play against computer");
        }
    }
}

