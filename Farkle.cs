using System;
using System.Collections.Generic;  

namespace ConsoleFarkle
{
    public enum RollResult {
            Nothing = 0,
            Pair = 1,
            Fives = 2,
            Ones = 3,
            TripTwos = 4,
            TripThrees = 5,
            TripOnes = 6,
            TripFours = 7,
            TripFives = 8,
            TripSixes = 9,
            FourOfAKind = 10,
            Straight = 11, 
            FourOfAKindAndPair = 12, 
            ThreePairs = 13,
            FiveOfAKind = 14,
            SixOfAKind = 15,
            TwoTriplets = 16,
    }
        
    class Farkle
    {
        Random random = new Random();

        public int[] roll(int rollCount) {
            int[] rollResults = new int[rollCount];
            for(int i = 0; i < rollCount; i++) {
                int num = random.Next(1,6);
                rollResults[i] = num;
            }
            return rollResults;
        }
        public List<RollResult> returnOptions(int[] roll) {
            List<RollResult> rollResults = new List<RollResult>();
            //Roll Number , Frequency
            Dictionary<int, int> resultTracker = new Dictionary<int, int>();
            Array.Sort(roll);
            for(int i = 0; i < roll.Length; i++) {
                if(resultTracker.ContainsKey(roll[i])) {
                    resultTracker[roll[i]] = ++resultTracker[roll[i]];
                } else {
                    resultTracker.Add(roll[i], 1);
                }
            }
            for(int i = 0; i < roll.Length; i++) {
                int currentKey = i;
                if(resultTracker.ContainsKey(i)) {
                    switch(resultTracker[currentKey]) {
                        case 1 : 
                            if(currentKey == 1) {
                                rollResults.Add(RollResult.Ones);
                            } else if(currentKey == 5) {
                                rollResults.Add(RollResult.Fives);
                            }
                            break;
                        case 2 : 
                            if(currentKey == 1) {
                                rollResults.Add(RollResult.Ones);
                                rollResults.Add(RollResult.Ones);
                            } else if(currentKey == 5) {
                                rollResults.Add(RollResult.Fives);
                                rollResults.Add(RollResult.Fives);
                            }
                            rollResults.Add(RollResult.Pair);
                            break;
                        case 3 :
                            rollResults.Add(getTripsResult(currentKey));
                            break;
                        case 4 :
                            rollResults.Add(RollResult.FourOfAKind);
                            break;
                        case 5 :
                            rollResults.Add(RollResult.FiveOfAKind);
                            break;
                        case 6 : 
                            rollResults.Add(RollResult.SixOfAKind);
                            break;
                        default :
                            break;
                    }
                }
            }
            int pairCount = 0;
            int tripletCount = 0;
            bool pair = false;
            bool fourOfAKind = false;
            foreach(RollResult result in rollResults) {
                if(result == RollResult.Pair) {
                    pair = true;
                    pairCount++;
                } else if(isTripsRollResult(result)) {
                    tripletCount++;
                } else if(result == RollResult.FourOfAKind) {
                    fourOfAKind = true;
                } 
            }
            if(pair && fourOfAKind) {
                rollResults.Add(RollResult.FourOfAKindAndPair);
            } else if(pairCount == 3) {
                rollResults.Add(RollResult.ThreePairs);
            } else if(tripletCount == 2) {
                rollResults.Add(RollResult.TwoTriplets);
            } 
            rollResults.RemoveAll(isPair);
            if(isStraight(roll)) {
                Console.WriteLine("isStriaght is true");
                rollResults.Add(RollResult.Straight);
            }
            if(rollResults.Count == 0) {
                // TODO: Cant be reached, pair will always be in there. Need to remove pairs when we get to this point
                rollResults.Add(RollResult.Nothing);
            }
            return rollResults;
        }
        private RollResult getTripsResult(int rollKey) {
            switch(rollKey) {
                case 1 :
                    return RollResult.TripOnes;
                case 2 :
                    return RollResult.TripTwos;
                case 3 :
                    return RollResult.TripThrees;
                case 4 :
                    return RollResult.TripFours;
                case 5 :
                    return RollResult.TripFives;
                case 6 :
                    return RollResult.TripSixes;
                default : 
                    return RollResult.TripOnes;
            }
        }

        private bool isTripsRollResult(RollResult result) {
            return (result == RollResult.TripOnes ||
                result == RollResult.TripTwos ||
                result == RollResult.TripThrees ||
                result == RollResult.TripFours ||
                result == RollResult.TripFives ||
                result == RollResult.TripSixes
            ) ? true : false;
        }
        private static bool isPair(RollResult roll) {
            return roll == RollResult.Pair;
        }
        private bool isStraight(int[] roll) {
            for(int i = 0; i < roll.Length; i++) {
                if(roll[i] != i + 1){
                    return false;
                }
            }
            return true;
        }
        public List<RollResult> determineRollSelections(string inputSelection, List<RollResult> results) {
            List<RollResult> selectedDiceOptions = new List<RollResult>();
            foreach(char input in inputSelection) {
                // TODO: check for invalid inputs, ex. spaces, numbers that are not in the list, invalid char...
                int numIndex = int.Parse(input.ToString()) - 1;
                selectedDiceOptions.Add(results[numIndex]);
            }
            return selectedDiceOptions;
        }
        public int calculateRemainingDice(List<RollResult> rollOptionsSelected, int currentDiceCount) {
            int remainingDice = currentDiceCount;
            foreach(RollResult rollOption in rollOptionsSelected) {
                // REMINDER: this roll result is just a int, dont think of it as an actual type.
                switch(rollOption) {
                    case RollResult.Fives : 
                        remainingDice--;
                        break;
                    case RollResult.Ones : 
                        remainingDice--;
                        break;
                    case RollResult.TripOnes :
                        remainingDice -= 3;
                        break;
                    case RollResult.TripTwos : 
                        remainingDice -= 3;
                        break;
                    case RollResult.TripThrees : 
                        remainingDice -= 3;
                        break;
                    case RollResult.TripFours : 
                        remainingDice -= 3;
                        break;
                    case RollResult.TripFives : 
                        remainingDice -= 3;
                        break;
                    case RollResult.TripSixes : 
                        remainingDice -= 3;
                        break; 
                    case RollResult.FourOfAKind : 
                        remainingDice -= 4;
                        break;
                    case RollResult.Straight : // TEST TO MAKE SURE YOU FIXED THIS BUG This also checks FourOfAKindAndPair and ThreePairs. They all remove the same amount of dice
                        remainingDice -= 6;
                        break;
                    case RollResult.FourOfAKindAndPair : 
                        remainingDice -= 6;
                        break;
                    case RollResult.ThreePairs : 
                        remainingDice -= 6;
                        break;
                    case RollResult.FiveOfAKind : 
                        remainingDice -= 5;
                        break;
                    case RollResult.SixOfAKind : 
                        remainingDice -= 6;
                        break; 
                    case RollResult.TwoTriplets : 
                        remainingDice -= 6;
                        break;  
                }
            }
            Console.WriteLine(remainingDice + "     Remainining dice");
            return remainingDice;
        }
        public int calculateScore(List<RollResult> rollOptionsSelected, int currentScore) {
            int newScore = currentScore;
            foreach(RollResult rollOption in rollOptionsSelected) {
                switch(rollOption) {
                    case RollResult.Fives : 
                        newScore += 50;
                        break;
                    case RollResult.Ones : 
                        newScore += 100;
                        break;
                    case RollResult.TripOnes :
                        newScore += 300;
                        break;
                    case RollResult.TripTwos : 
                        newScore += 200;
                        break;
                    case RollResult.TripThrees : 
                        newScore += 300;
                        break;
                    case RollResult.TripFours : 
                        newScore += 400;
                        break;
                    case RollResult.TripFives : 
                        newScore += 500;
                        break;
                    case RollResult.TripSixes : 
                        newScore += 600;
                        break; 
                    case RollResult.FourOfAKind : 
                        newScore += 1000;
                        break;
                    case RollResult.Straight : // TEST TO MAKE SURE YOU FIXED THIS BUG This also checks FourOfAKindAndPair and ThreePairs. They all remove the same amount of dice
                        newScore += 1500;
                        break;
                    case RollResult.FourOfAKindAndPair : 
                        newScore += 1500;
                        break;
                    case RollResult.ThreePairs : 
                        newScore += 1500;
                        break;
                    case RollResult.FiveOfAKind : 
                        newScore += 2000;
                        break;
                    case RollResult.SixOfAKind : 
                        newScore += 3000;
                        break; 
                    case RollResult.TwoTriplets : 
                        newScore += 2500;
                        break;  
                }
            }
            return newScore;
        }
    }
}
            // Nothing = 0,
            // Pair = 1,
            // SixOfAKind = 3000,
            // TwoTriplets = 2500,