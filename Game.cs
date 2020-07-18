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
            int winningScore = 5000;
            bool player1Turn = true;
            int startDice = 6;
            int player1Score = 0;
            int player2Score = 0;
            while(player1Score <= winningScore && player2Score <= winningScore) {
                if(player1Turn) {
                    // TODO: fix this, I am adding score but then when I farkle i add 0, play turn either needs to handle the whole turn which it can.
                    Console.WriteLine("Player 1 your turn! Score: {0}", player1Score);
                    TurnResult turnResult = playTurn(startDice, player1Score);
                    if(!turnResult.didFarkle) {
                        player1Score += turnResult.score;
                    }
                    player1Turn = false;
                } else {
                    Console.WriteLine("Player 2 your turn! Score: {0}", player2Score);
                    TurnResult turnResult = playTurn(startDice, player2Score);
                    if(!turnResult.didFarkle) {
                        player2Score += turnResult.score;
                    }
                    player1Turn = true;
                }
            }
        }
        public RollResult playRoll(int startDice, int turnScore) {
            RollResult turnResult;
            Farkle farkle = new Farkle();
            int currentDiceCount = startDice;
            int currentScore = turnScore;
            int[] diceRolls = farkle.roll(currentDiceCount);
            List<RollOption> rollResults = farkle.returnOptions(diceRolls);
            foreach(int roll in diceRolls) {
                Console.Write("{0} | ", roll);
            }
            Console.WriteLine(" ");
            Console.WriteLine("_________________");
            for(int i = 0; i < rollResults.Count; i++) {
                Console.WriteLine("{0}. {1}", i + 1, rollResults[i]);
            }
            if(rollResults[0] == RollOption.Nothing) {
                Console.WriteLine("FARKLE!");
                turnResult = new RollResult(0, true, true, currentDiceCount);
                return turnResult;
            }
            Console.WriteLine("Enter numbers of what options you want to take. If you want to take the roll, end your input with a /");
            Console.WriteLine("_______________");
            string input = Console.ReadLine();
            List<RollOption> rollResultsSelected = farkle.determineRollSelections(input, rollResults);
            currentScore = farkle.calculateScore(rollResultsSelected, turnScore);
            currentDiceCount = farkle.calculateRemainingDice(rollResultsSelected, currentDiceCount);
            bool endTurnTakeScore = (rollResultsSelected[rollResultsSelected.Count - 1] == RollOption.TakeScore ? true : false);
            turnResult = new RollResult(currentScore, endTurnTakeScore, false, currentDiceCount);
            Console.WriteLine("\n\n\n");
            return turnResult;
        }

        public TurnResult playTurn(int startDice, int playerScore) {
            int currentDiceCount = startDice;
            int currentScore = playerScore;
            Console.WriteLine("Turn Score: {0}", currentScore);
            if(currentDiceCount == 0) {
                currentDiceCount = 6;
            }
            RollResult rollResult = playRoll(startDice, playerScore);
            TurnResult turnResult;
            if(rollResult.endTurn && rollResult.farkle) {
                turnResult = new TurnResult(currentScore, true);
                return turnResult;
            } else if(rollResult.endTurn && !rollResult.farkle) {
                currentScore += rollResult.score;
                turnResult = new TurnResult(currentScore, false);
                return turnResult;
            }
                currentScore += rollResult.score;
                currentDiceCount = rollResult.remainingDice;
                turnResult = playTurn(currentDiceCount, currentScore);

            return turnResult;
        }
        public void computerGame() {
            Console.WriteLine("Play against computer");
        }
    }
}

