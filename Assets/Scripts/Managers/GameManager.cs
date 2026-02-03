using UnityEngine;
using System.Collections;
using System;
using SpaceGame;
using System.Collections.Generic;

namespace SpaceGame
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game Entities")]
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject[] enemyPrefab;
        [SerializeField] private EnemySpawn[] enemySpawns;
        private List<GameObject> enemyPool = new List<GameObject>();
        GameObject chosenEnemy;
        [SerializeField] private Transform[] spawnPosition;

        [Header("Game Variables")]
        [SerializeField] private float enemySpawnRate = 0.5f;
        private float enemySpawnCount = 0;

        public Action onGameStart;
        public Action onGameOver;
        private bool isPlaying = false;

        public PickupManager pickupManager;
        public ScoreManager scoreManager;

        public UiManager uiManager;
        public CameraManager cameraManager;

        private Player player;
        private GameObject tempEnemy;
        private bool isEnemySpawning;
        private float powerUpTimer = 0f;

        public Action<int> timerInterval;
        public Action<int> powerUpStarted;
        public Action powerUpEnded;

        /// <summary>
        /// Singleton GameManager to manage game state and transitions
        /// </summary>
        
        #region Singletone Code
        private static GameManager instance;
        public static GameManager GetInstance()
        {
            return instance;
        }

        void SetSingleton()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;
        }
        #endregion
        private void Awake()
        {
            SetSingleton();
            // DontDestroyOnLoad(this.gameObject);
        }
    
        IEnumerator EnemySpawner()
        {
            while (isEnemySpawning)
            {
                yield return new WaitForSeconds(1 / enemySpawnRate);
                if (isEnemySpawning)
                {
                    CreateEnemy();
                    enemySpawnCount++;
                }
            }
        }

        public void NotifyDeath(Enemy enemy)
        {
            // scoreManager.IncrementScore(20);
            StartCoroutine(DelayedPickupSpawn(enemy.transform.position));
            // pickupManager.SpawnPickup(enemy.transform.position);
        }

        IEnumerator DelayedPickupSpawn(Vector3 position)
        {
            yield return new WaitForSeconds(1f);
            pickupManager.SpawnPickup(position);
        }

        void CreateEnemy()
        {
            if (enemyPool.Count <= 0)
                return;
            chosenEnemy = enemyPool[UnityEngine.Random.Range(0, enemyPool.Count)];
            tempEnemy = Instantiate(chosenEnemy);
            tempEnemy.transform.position = spawnPosition[UnityEngine.Random.Range(0, spawnPosition.Length)].position;
            // tempEnemy.GetComponent<Enemy>().weapon = meleeWeapon;
            // tempEnemy.GetComponent<Enemy>().SetEnemyType(EnemyType.Melee);
            // tempEnemy.GetComponent<EnemyMelee>().SetMeleeEnemy(2, 0.25f);
        }

        public void FindPlayers()
        {
            try
            {
                player = GameObject.FindWithTag("Player").GetComponent<Player>();
            }
            catch (NullReferenceException e)
            {
                Debug.Log("Player not found: " + e.Message);
            }
        }

        public Player GetPlayer() { return player; }

        public bool IsPlaying()
        {
            return isPlaying;
        }

        public void StartGame()
        {
            player = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity).GetComponent<Player>();
            player.OnDeath += StopGame;
            isPlaying = true;

            SetUpEnemySpawns();

            onGameStart?.Invoke();
            StartCoroutine(GameStarter());
        }

        private void SetUpEnemySpawns()
        {
            foreach (EnemySpawn spawn in enemySpawns)
            {
                for(int i = 0; i < spawn.spawnWeight; i++)
                {
                    enemyPool.Add(spawn.enemy);
                }
            }
        }

        IEnumerator GameStarter()
        {
            yield return new WaitForSeconds(2.0f);
            isEnemySpawning = true;
            StartCoroutine(EnemySpawner());
        }

        public void StopGame()
        {
            isEnemySpawning = false;
            scoreManager.SetHighScore();

            StartCoroutine(GameStopper());
        }

        IEnumerator GameStopper()
        {
            print("Game Over");
            isEnemySpawning = false;
            yield return new WaitForSeconds(2.0f);
            isPlaying = false;

            foreach (Enemy item in FindObjectsByType(typeof(Enemy), FindObjectsSortMode.InstanceID))
            {
                Destroy(item.gameObject);
            }

            foreach (PickUp item in FindObjectsByType(typeof(PickUp), FindObjectsSortMode.InstanceID))
            {
                Destroy(item.gameObject);
            }

            onGameOver?.Invoke();
        }

        void Update()
        {
            if (powerUpTimer > 0f)
            {
                powerUpTimer -= Time.deltaTime; // Each frame, reduce the timer by the time passed since last frame
                if (uiManager.currentTimer != Mathf.CeilToInt(powerUpTimer))
                {
                    timerInterval?.Invoke(Mathf.CeilToInt(powerUpTimer));

                }
                if (powerUpTimer < 0f)
                {
                    powerUpTimer = 0f;
                    powerUpEnded?.Invoke(); // Invoke the event to signal that the power-up has ended
                }
            }

            if (enemySpawnCount > 5)
            {
                if(UnityEngine.Random.Range(0, 10) < enemySpawnCount+2 - 5)
                {
                    enemySpawnRate += 0.1f;
                    enemySpawnCount = 0;
                }
            }
        }

        public void SetPowerUpTimer(float time)
        {
            powerUpTimer = time; // Timer is set to time, counting down until it runs out.
            powerUpStarted?.Invoke(Mathf.RoundToInt(powerUpTimer)); // Invoke the event to signal that the power-up has started
        }

        public void ActivatecNuke(GameObject effect)
        {
            foreach (Enemy item in FindObjectsByType(typeof(Enemy), FindObjectsSortMode.InstanceID))
            {
                GameObject explosionVFX = Instantiate(effect, item.transform.position, Quaternion.identity);
                Destroy(explosionVFX, 3.5f);
                Destroy(item.gameObject); //Not die so you can't chain pickups
                scoreManager.IncrementScore(10);
            }

            cameraManager.ShakeCamera(3f, 5f, 8, 7);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

    }
}

[System.Serializable]
public struct EnemySpawn
{
    public GameObject enemy;
    public int spawnWeight;
}