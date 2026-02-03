using UnityEngine;

namespace SpaceGame
{
    public class EnemyShooter : Enemy
    {
        [Header("Values")]
        [SerializeField] private float attackRange;
        [SerializeField] private float attackInterval;

        [SerializeField] private Bullet bulletPrefab;
        
        private float timer;

        enum EnemyStates
        {
            Follow,
            Shooting
        }
        private EnemyStates enemyState = EnemyStates.Follow;

        protected override void Start()
        {
            base.Start();
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
                    if (Vector2.Distance(transform.position, target.position) < attackRange)
                    {
                        enemyState = EnemyStates.Shooting;
                    }
                    break;
                case EnemyStates.Shooting:
                    base.Update();
                    if (timer > attackInterval)
                    {
                        Shoot();
                        timer = 0;
                    }
                    break;

            }
        }


        public override void Shoot()
        {
            weapon.Shoot(bulletPrefab, this, "Player");
        }

    }
}