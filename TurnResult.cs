using System;


namespace ConsoleFarkle
{
    class TurnResult
    {
        public int score;
        public bool didFarkle;
        public TurnResult(int score, bool didFarkle) {
            this.score = score;
            this.didFarkle = didFarkle;
        }
    }
}
