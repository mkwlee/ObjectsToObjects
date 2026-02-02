using UnityEngine;
using UnityEngine.Events;


namespace SpaceGame
{
    public class ScoreManager : MonoBehaviour
    {
        private int seconds;
        private int score;
        private int highScore;
        
        public UnityEvent OnScoreUpdate;
        public UnityEvent OnHighScoreUpdate;

        private void Start()
        {
            highScore = PlayerPrefs.GetInt("HighScore", 0);
            GameManager.GetInstance().onGameStart += OnGameStart;
        }

        private void OnGameStart()
        {
            score = 0;
            OnHighScoreUpdate?.Invoke();
        }

        public void SetHighScore()
        {
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        public string timer
        {
            get
            {
                return Mathf.Round((float)seconds / 60.0f) + "Mins and " + seconds % 60 + "Seconds";
            }
            private set {  }
        }

        public int GetHighScore()
        {
            return highScore;
        }
        public int GetScore()
        {
            return score;
        }

        public void IncrementScore()
        {
            score++;
            OnScoreUpdate?.Invoke();

            if (score > highScore)
            {
                highScore = score;
                OnHighScoreUpdate?.Invoke();
            }
        }
    }
    
}