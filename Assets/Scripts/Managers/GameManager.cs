using UnityEngine;
using System.Collections;
using System;

namespace SpaceGame
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game Entities")]
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject[] enemyPrefab;
        [SerializeField] private Transform[] spawnPosition;

        [Header("Game Variables")]
        [SerializeField] private float enemySpawnRate = 0.5f;

        public Action onGameStart;
        public Action onGameOver;
        private bool isPlaying = false;

        public PickupManager pickupManager;
        public ScoreManager scoreManager;

        private Player player;
        private GameObject tempEnemy;
        private bool isEnemySpawning;
        private Weapon meleeWeapon = new Weapon("Melee", 1, 0);

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

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        // void Start()
        // {
        //     isEnemySpawning = true;
        //     FindPlayers();
        //     StartCoroutine(EnemySpawner());
        
        // }

        // Update is called once per frame
        void Update()
        {
            
        }

        IEnumerator EnemySpawner()
        {
            while (isEnemySpawning)
            {
                yield return new WaitForSeconds(1 / enemySpawnRate);
                CreateEnemy();
            }
        }

        public void NotifyDeath(Enemy enemy)
        {
            pickupManager.SpawnPickup(enemy.transform.position);
        }

        void CreateEnemy()
        {
            tempEnemy = Instantiate(enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Length)]);
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

            onGameStart?.Invoke();
            StartCoroutine(GameStarter());
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

    }
}