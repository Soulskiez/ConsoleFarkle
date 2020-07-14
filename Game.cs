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
            Console.WriteLine("Play against another player");
            Farkle farkle = new Farkle();
            int[] rollTest = farkle.roll(6);
            List<RollResult> rollResults = farkle.returnOptions(rollTest);
            Console.WriteLine("Roll test");
            foreach(int roll in rollTest) {
                Console.WriteLine(roll);
            }
            Console.WriteLine("_________");
            for(int i = 0; i < rollResults.Count; i++) {
                Console.WriteLine((i + 1) + ". " + rollResults[i]);
            }
            Console.WriteLine("Enter numbers of what options you want to take.");
            Console.WriteLine("_________");
            string input = Console.ReadLine();
            Console.WriteLine(input + " this was the input");
            List<RollResult> rollResultsSelected = farkle.determineRollSelections(input, rollResults);
            foreach(RollResult result in rollResultsSelected) {
                Console.WriteLine(result);
            }
        }
        public void computerGame() {
            Console.WriteLine("Play against computer");
        }
    }
}

