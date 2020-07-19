using System;
using System.Collections.Generic;  

namespace ConsoleFarkle
{
    class Game
    {
        public void startGame()
        {
            Console.WriteLine("Welcome to Console Farkle. \n \n \n ");
            twoPlayerGame();
        }

        public void twoPlayerGame() {
            int winningScore = 5000;
            bool player1Turn = true;
            int startDice = 6;
            int player1Score = 0;
            int player2Score = 0;
            while(playerHasntWon(player1Score, player2Score, winningScore)) { 
                if(player1Turn) {
                    Console.WriteLine("Player 1 your turn! Score: {0}", player1Score);
                    TurnResult turnResult = playTurn(startDice, 0);
                    if(!turnResult.didFarkle) {
                        player1Score += turnResult.score;
                    }
                    player1Turn = false;
                } else {
                    Console.WriteLine("Player 2 your turn! Score: {0}", player2Score);
                    TurnResult turnResult = playTurn(startDice, 0);
                    if(!turnResult.didFarkle) {
                        player2Score += turnResult.score;
                    }
                    player1Turn = true;
                }
            }
            Console.WriteLine("p1 score: {0}, p2 score: {1}, gameover", player1Score, player2Score);
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
            RollResult rollResult = playRoll(currentDiceCount, currentScore);
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
        public bool playerHasntWon(int p1Score, int p2Score, int winCondition) {
            if(p1Score >= winCondition) {
                return false;
            } else if(p2Score >= winCondition) {
                return false;
            } else {
                return true;
            }
        }
    }
}

