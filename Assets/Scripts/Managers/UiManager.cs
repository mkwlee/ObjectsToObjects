using TMPro;
using UnityEngine;


namespace SpaceGame
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text txtHealth, txtScore, txtHighScore;
        [SerializeField] private GameObject startMenu, endMenu;
        Player player;
        ScoreManager scoreManager;


        void Start()
        {
            scoreManager = GameManager.GetInstance().scoreManager;

            GameManager.GetInstance().onGameStart += GameStarted;
            GameManager.GetInstance().onGameOver += GameOver;
        }

        public void GameStarted()
        {
            player = GameManager.GetInstance().GetPlayer();
            player.health.OnHealthUpdate += UpdateHealth;
            UpdateHealth(player.health.GetHealth());
        }

        public void GameOver()
        {
            startMenu.SetActive(true);
            endMenu.SetActive(true);
        }

        public void UpdateHealth(int currentHealth)
        {
            txtHealth.SetText(currentHealth.ToString());
        }

        public void UpdateScore()
        {
            txtScore.SetText(GameManager.GetInstance()?.scoreManager.GetScore().ToString());
        }

        public void UpdateHightScore()
        {
            // txtHighScore.SetText(ScoreManager.GetHighScore().ToString());
        }
    }
}