using DG.Tweening;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceGame
{
    public class EnemyExploder : Enemy
    {

        [SerializeField] private float fuseTime;
        [SerializeField] private float explosionRange;
        [SerializeField] private int explosioDetection;

        [SerializeField] private GameObject explosionEffect;
        [SerializeField] private bool isGrenade = false;
        private SpriteRenderer spriteRenderer;
        private Color regularColor = new Color(255, 231, 0, 255);

        enum EnemyStates
        {
            Follow,
            Fuse
        }
        private EnemyStates enemyState = EnemyStates.Follow;

        private float timer;

        protected override void Start()
        {
            base.Start();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void Update()
        {
            if (target == null)
                return;

            timer += Time.deltaTime;
            switch(enemyState)
            {
                case EnemyStates.Follow:
                    base.Update();
                    if (Vector2.Distance(transform.position, target.position) < explosioDetection)
                    {
                        enemyState = EnemyStates.Fuse;
                        spriteRenderer.DOKill();
                        spriteRenderer.DOColor(Color.white, fuseTime-0.05f);
                        speed = 0;
                        timer = 0;
                    }
                    break;
                case EnemyStates.Fuse:
                    base.Update();
                    if (timer > fuseTime)
                    {
                        Attack(0);
                    }

                    // if (Vector2.Distance(transform.position, target.position) > explosionRange)
                    // {
                    //     enemyState = EnemyStates.Follow;
                    //     spriteRenderer.DOKill();
                    //     spriteRenderer.DOColor(regularColor, 0);
                    //     speed = maxSpeed;
                    // }
                    break;
            }
        }

        public override void Attack(float interval)
        {
            if (Vector2.Distance(transform.position, target.position) < explosionRange)
            {
                Shield currentShield = GameManager.GetInstance().GetPlayer().currentShield;
                if (currentShield != null && Vector2.Distance(transform.position, currentShield.transform.position) < Vector2.Distance(transform.position, target.position))
                {
                    currentShield.GetDamage(weapon.GetDamage());
                }
                else
                {
                    target.GetComponent<IDamageable>().GetDamage(weapon.GetDamage());
                }
                
            }
            if (isGrenade)
            {
                GameManager.GetInstance().cameraManager.ShakeCamera(1f, 3f, 8, 5);
            }
            else
            {
                GameManager.GetInstance().cameraManager.ShakeCamera(1f, 1.5f, 4, 4);
            }
            
            spriteRenderer.DOKill();
            GameObject explosionVFX = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(explosionVFX, 3.5f);
            Destroy(gameObject);
        }
        
        private void PrintHealth()
        {
            Debug.Log($"Enemy Health: {health.GetHealth()}");
        }

        #if UNITY_EDITOR
            // This will create a button in the Inspector

            // Optional: Draw a proper button in Inspector using this trick
            [CustomEditor(typeof(EnemyExploder))]
            public class EnemyExploderEditor : Editor
            {
                public override void OnInspectorGUI()
                {
                    base.OnInspectorGUI();

                    if (GUILayout.Button("PrintHealth"))
                    {
                        (target as EnemyExploder).PrintHealth();
                    }
                }
            }
        #endif

    }
}
