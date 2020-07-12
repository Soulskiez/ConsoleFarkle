using System;

namespace ConsoleFarkle
{
    class Farkle
    {
        // int fives = 50;
        // int ones = 100;
        // int tripTwos = 200;
        // int tripThrees, tripOnes = 300;
        // int tripFours = 400;
        // int tripFives = 500;
        // int tripSixes = 600;
        // int fourOfAKind = 1000;
        // int straight, fourOfAKindAndPair, threePairs = 1500;
        // int fiveOfAKind = 2000;
        // int sixOfAKind = 3000;
        // int twoTriplets = 2500;

        Random random = new Random();

        public int[] roll() {
            int[] rollResults = new int[6];
            for(int i = 0; i < 6; i++) {
                int num = random.Next(1,6);
                rollResults[i] = num;
            }
            return rollResults;
        }
    }
}
