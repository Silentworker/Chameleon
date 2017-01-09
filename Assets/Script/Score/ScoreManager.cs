using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Score
{
    public class ScoreManager : MonoBehaviour, IScoreManager
    {
        private Text _text;
        private int _score;

        void Start()
        {
            _text = GetComponent<Text>();
            SetScore(0);
        }

        private void SetScore(int score)
        {
            _score = score;
            if (_score < 0) _score = 0;

            _text.text = (_score < 10 ? "00" : _score < 100 ? "0" : "") + _score;
        }

        public void AddScore(int score)
        {
            SetScore(_score + score);
        }

        public void ReduceScore(int score)
        {
            SetScore(_score - score);
        }
    }
}
