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
                    twoPlayerGame(6, 0);
                    break;
                case 2 : 
                    computerGame();
                    break;
                default : 
                    Console.WriteLine("You didn't enter a valid option");
                    break;
            }
        }

        public void twoPlayerGame(int startDice, int score) {
            if(score >= 5000) { // Change later after testing.
                Console.WriteLine("you win!");
                return;
            }
            int currentScore = score;
            Farkle farkle = new Farkle();
            int diceCount = startDice;
            int[] rollTest = farkle.roll(diceCount);
            //int[] rollTest = {1,1,2,2,3,3};
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
                Console.WriteLine("Your a Loser");
                return;
            }
            Console.WriteLine("Enter numbers of what options you want to take.");
            Console.WriteLine("_________");
            string input = Console.ReadLine();
            Console.WriteLine(input + " this was the input");
            List<RollResult> rollResultsSelected = farkle.determineRollSelections(input, rollResults);
            currentScore = farkle.calculateScore(rollResultsSelected, currentScore);
            int diceForNextRoll = farkle.calculateRemainingDice(rollResultsSelected, diceCount);
            Console.WriteLine("Here is the remaining dice count {0}, Current Score: {1}", diceForNextRoll, currentScore);
            twoPlayerGame(diceForNextRoll, currentScore);
        }
        public void computerGame() {
            Console.WriteLine("Play against computer");
        }
    }
}

