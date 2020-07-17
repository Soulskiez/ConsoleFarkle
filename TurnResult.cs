using System;


namespace ConsoleFarkle
{
    class TurnResult
    {
        public int score;
        public bool endTurn;
        public bool farkle;
        public TurnResult(int score, bool endTurn, bool farkle) {
            this.score = score;
            this.endTurn = endTurn;
            this.farkle = farkle;
        }
        public void setScore(int score) {
            score = this.score;
        }
        public int getScore() {
            return score;
        }
        public void setEndTurn(bool endTurn) {
            endTurn = this.endTurn;
        }
        public bool getEndTurn() {
            return endTurn;
        }
        public void setFarkle(bool farkle) {
            farkle = this.farkle;
        }
        public bool getFarkle() {
            return farkle;
        }
    }
}
