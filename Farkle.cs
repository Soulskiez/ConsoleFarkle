using System;
using System.Collections.Generic;  

namespace ConsoleFarkle
{
    public enum RollOption {
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
            TakeScore = 17,
    }
        
    class Farkle
    {
        Random random = new Random();

        public int[] roll(int rollCount) {
            int[] rollOptions = new int[rollCount];
            for(int i = 0; i < rollCount; i++) {
                int num = random.Next(1,6);
                rollOptions[i] = num;
            }
            return rollOptions;
        }
        public List<RollOption> returnOptions(int[] roll) {
            List<RollOption> rollOptions = new List<RollOption>();
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
                                rollOptions.Add(RollOption.Ones);
                            } else if(currentKey == 5) {
                                rollOptions.Add(RollOption.Fives);
                            }
                            break;
                        case 2 : 
                            if(currentKey == 1) {
                                rollOptions.Add(RollOption.Ones);
                                rollOptions.Add(RollOption.Ones);
                            } else if(currentKey == 5) {
                                rollOptions.Add(RollOption.Fives);
                                rollOptions.Add(RollOption.Fives);
                            }
                            rollOptions.Add(RollOption.Pair);
                            break;
                        case 3 :
                            rollOptions.Add(getTripsResult(currentKey));
                            break;
                        case 4 :
                            rollOptions.Add(RollOption.FourOfAKind);
                            break;
                        case 5 :
                            rollOptions.Add(RollOption.FiveOfAKind);
                            break;
                        case 6 : 
                            rollOptions.Add(RollOption.SixOfAKind);
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
            foreach(RollOption result in rollOptions) {
                if(result == RollOption.Pair) {
                    pair = true;
                    pairCount++;
                } else if(isTripsRollOption(result)) {
                    tripletCount++;
                } else if(result == RollOption.FourOfAKind) {
                    fourOfAKind = true;
                } 
            }
            if(pair && fourOfAKind) {
                rollOptions.Add(RollOption.FourOfAKindAndPair);
            } else if(pairCount == 3) {
                rollOptions.Add(RollOption.ThreePairs);
            } else if(tripletCount == 2) {
                rollOptions.Add(RollOption.TwoTriplets);
            } 
            rollOptions.RemoveAll(isPair);
            if(isStraight(roll)) {
                rollOptions.Add(RollOption.Straight);
            }
            if(rollOptions.Count == 0) {
                rollOptions.Add(RollOption.Nothing);
            }
            return rollOptions;
        }
        private RollOption getTripsResult(int rollKey) {
            switch(rollKey) {
                case 1 :
                    return RollOption.TripOnes;
                case 2 :
                    return RollOption.TripTwos;
                case 3 :
                    return RollOption.TripThrees;
                case 4 :
                    return RollOption.TripFours;
                case 5 :
                    return RollOption.TripFives;
                case 6 :
                    return RollOption.TripSixes;
                default : 
                    return RollOption.TripOnes;
            }
        }

        private bool isTripsRollOption(RollOption result) {
            return (result == RollOption.TripOnes ||
                result == RollOption.TripTwos ||
                result == RollOption.TripThrees ||
                result == RollOption.TripFours ||
                result == RollOption.TripFives ||
                result == RollOption.TripSixes
            ) ? true : false;
        }
        private static bool isPair(RollOption roll) {
            return roll == RollOption.Pair;
        }
        private bool isStraight(int[] roll) {
            if(roll.Length != 6) {
                return false;
            }
            for(int i = 0; i < roll.Length; i++) {
                if(roll[i] != i + 1){
                    return false;
                }
            }
            return true;
        }
        public List<RollOption> determineRollSelections(string inputSelection, List<RollOption> results) {
            List<RollOption> selectedDiceOptions = new List<RollOption>();
            foreach(char input in inputSelection) {
                // TODO: check for invalid inputs, ex. spaces, numbers that are not in the list, invalid char...
                if(input == '/') {
                    selectedDiceOptions.Add(RollOption.TakeScore);
                } else {
                    int numIndex = int.Parse(input.ToString()) - 1;
                    selectedDiceOptions.Add(results[numIndex]);
                }
            }
            return selectedDiceOptions;
        }
        public int calculateRemainingDice(List<RollOption> rollOptionsSelected, int currentDiceCount) {
            int remainingDice = currentDiceCount;
            foreach(RollOption rollOption in rollOptionsSelected) {
                switch(rollOption) {
                    case RollOption.Fives : 
                        remainingDice--;
                        break;
                    case RollOption.Ones : 
                        remainingDice--;
                        break;
                    case RollOption.TripOnes :
                        remainingDice -= 3;
                        break;
                    case RollOption.TripTwos : 
                        remainingDice -= 3;
                        break;
                    case RollOption.TripThrees : 
                        remainingDice -= 3;
                        break;
                    case RollOption.TripFours : 
                        remainingDice -= 3;
                        break;
                    case RollOption.TripFives : 
                        remainingDice -= 3;
                        break;
                    case RollOption.TripSixes : 
                        remainingDice -= 3;
                        break; 
                    case RollOption.FourOfAKind : 
                        remainingDice -= 4;
                        break;
                    case RollOption.Straight : 
                        remainingDice -= 6;
                        break;
                    case RollOption.FourOfAKindAndPair : 
                        remainingDice -= 6;
                        break;
                    case RollOption.ThreePairs : 
                        remainingDice -= 6;
                        break;
                    case RollOption.FiveOfAKind : 
                        remainingDice -= 5;
                        break;
                    case RollOption.SixOfAKind : 
                        remainingDice -= 6;
                        break; 
                    case RollOption.TwoTriplets : 
                        remainingDice -= 6;
                        break;  
                }
            }
            return remainingDice;
        }
        public int calculateScore(List<RollOption> rollOptionsSelected, int currentScore) {
            int newScore = currentScore;
            foreach(RollOption rollOption in rollOptionsSelected) {
                switch(rollOption) {
                    case RollOption.Fives : 
                        newScore += 50;
                        break;
                    case RollOption.Ones : 
                        newScore += 100;
                        break;
                    case RollOption.TripOnes :
                        newScore += 300;
                        break;
                    case RollOption.TripTwos : 
                        newScore += 200;
                        break;
                    case RollOption.TripThrees : 
                        newScore += 300;
                        break;
                    case RollOption.TripFours : 
                        newScore += 400;
                        break;
                    case RollOption.TripFives : 
                        newScore += 500;
                        break;
                    case RollOption.TripSixes : 
                        newScore += 600;
                        break; 
                    case RollOption.FourOfAKind : 
                        newScore += 1000;
                        break;
                    case RollOption.Straight :  
                        newScore += 1500;
                        break;
                    case RollOption.FourOfAKindAndPair : 
                        newScore += 1500;
                        break;
                    case RollOption.ThreePairs : 
                        newScore += 1500;
                        break;
                    case RollOption.FiveOfAKind : 
                        newScore += 2000;
                        break;
                    case RollOption.SixOfAKind : 
                        newScore += 3000;
                        break; 
                    case RollOption.TwoTriplets : 
                        newScore += 2500;
                        break;  
                }
            }
            return newScore;
        }
    }
}