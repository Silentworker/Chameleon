﻿namespace Assets.Script.Score
{
    public interface IScoreManager
    {
        void AddScore(int score);

        void ReduceScore(int score);
    }
}