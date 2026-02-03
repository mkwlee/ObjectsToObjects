using TMPro;
using UnityEngine;


namespace SpaceGame
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text txtHealth, txtScore, txtHighScore, txtPowerUpTimer;
        [SerializeField] private GameObject startMenu, endMenu;
        Player player;
        ScoreManager scoreManager;

        public int currentTimer = 0;


        void Start()
        {
            scoreManager = GameManager.GetInstance().scoreManager;

            GameManager.GetInstance().onGameStart += GameStarted;
            GameManager.GetInstance().onGameOver += GameOver;
            GameManager.GetInstance().powerUpStarted += PowerUpUIEnabled;
            GameManager.GetInstance().powerUpEnded += PowerUpUIDisabled;
            GameManager.GetInstance().timerInterval += UpdatePowerUpTimer;
        }

        public void GameStarted()
        {
            player = GameManager.GetInstance().GetPlayer();
            player.health.OnHealthUpdate += UpdateHealth;
            UpdateHealth(player.health.GetHealth());
        }

        public void GameOver()
        {
            // startMenu.SetActive(true);
            endMenu.SetActive(true);
        }

        public void UpdateHealth(int currentHealth)
        {
            txtHealth.SetText(currentHealth.ToString("D3"));
        }

        public void UpdateScore()
        {
            txtScore.SetText(GameManager.GetInstance()?.scoreManager.GetScore().ToString("D5"));
        }

        public void UpdateHightScore()
        {
            txtHighScore.SetText(
                "High Score\n\n<size=72><color=#999999>" +
                scoreManager.GetHighScore().ToString("D5") +
                "</color></size>"
            );
        }

        public void PowerUpUIEnabled(int timerValue)
        {
            print("Enable power-up UI elements");
            txtPowerUpTimer.gameObject.SetActive(true);
            currentTimer = timerValue;
            txtPowerUpTimer.SetText(timerValue.ToString());
        }

        public void PowerUpUIDisabled()
        {
            print("Disable power-up UI elements");
            txtPowerUpTimer.gameObject.SetActive(false);
        }

        public void UpdatePowerUpTimer(int timerValue)
        {
            currentTimer = timerValue;
            txtPowerUpTimer.SetText(timerValue.ToString());
        }
    }
}