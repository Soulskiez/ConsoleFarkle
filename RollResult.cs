using System;


namespace ConsoleFarkle
{
    class RollResult
    {
        public int score;
        public bool endTurn;
        public bool farkle;
        public int remainingDice; 

        public RollResult(int score, bool endTurn, bool farkle, int remainingDice) {
            this.score = score;
            this.endTurn = endTurn;
            this.farkle = farkle;
            this.remainingDice = remainingDice;
        }
    }
}
