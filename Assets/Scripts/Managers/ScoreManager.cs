using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;


#if UNITY_EDITOR
using UnityEditor;
#endif

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

        public void IncrementScore(int amount = 5)
        {
            score += amount;
            OnScoreUpdate?.Invoke();

            if (score > highScore)
            {
                highScore = score;
                OnHighScoreUpdate?.Invoke();
            }
        }
    }

        #if UNITY_EDITOR
            // This will create a button in the Inspector

            // Optional: Draw a proper button in Inspector using this trick
            [CustomEditor(typeof(ScoreManager))]
            public class CameraEditor : Editor
            {
                public override void OnInspectorGUI()
                {
                    base.OnInspectorGUI();

                    if (GUILayout.Button("Reset High Score"))
                    {
                        PlayerPrefs.SetInt("HighScore", 0);
                    }
                }
            }
        #endif
    
}