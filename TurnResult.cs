using System;


namespace ConsoleFarkle
{
    class TurnResult
    {
        public int score;
        public bool endTurn;
        public TurnResult(int score, bool endTurn) {
            score = this.score;
            endTurn = this.endTurn;
        }
    }
}
