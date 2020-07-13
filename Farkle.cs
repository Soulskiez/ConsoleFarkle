using System;
using System.Collections.Generic;  

namespace ConsoleFarkle
{
    public enum RollResult {
            Pair, Nothing = 0,
            Fives = 50,
            Ones = 100,
            TripTwos = 200,
            TripThrees, TripOnes = 300,
            TripFours = 400,
            TripFives = 500,
            TripSixes = 600,
            FourOfAKind = 1000,
            Straight, FourOfAKindAndPair, ThreePairs = 1500,
            FiveOfAKind = 2000,
            SixOfAKind = 3000,
            TwoTriplets = 2500,
        }
    class Farkle
    {
        Random random = new Random();

        public int[] roll() {
            int[] rollResults = new int[6];
            for(int i = 0; i < 6; i++) {
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
            bool isStraightPossible = true;
            if(rollResults.Count == 0) {
                for(int i = 0; i < roll.Length; i++) {
                    if(roll[i] != i) {
                        isStraightPossible = false;
                        break;
                    } 
                }
                if(isStraightPossible) {
                    rollResults.Add(RollResult.Straight);
                } else {
                    // TODO: Cant be reached, pair will always be in there. Need to remove pairs when we get to this point
                    rollResults.Add(RollResult.Nothing);
                }
            }
            return rollResults;
        }
        public RollResult getTripsResult(int rollKey) {
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

        public bool isTripsRollResult(RollResult result) {
            return (result == RollResult.TripOnes ||
                result == RollResult.TripTwos ||
                result == RollResult.TripThrees ||
                result == RollResult.TripFours ||
                result == RollResult.TripFives ||
                result == RollResult.TripSixes
            ) ? true : false;
        }
    }
}
