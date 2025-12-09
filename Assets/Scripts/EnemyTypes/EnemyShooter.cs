using UnityEngine;

namespace SpaceGame
{
    public class EnemyShooter : Enemy
    {
        [Header("Values")]
        [SerializeField] private float circleRange;
        [SerializeField] private float attackRange;
        [SerializeField] private float attackTime;

        [SerializeField] private Bullet bulletPrefab;
        
        private float timer;

        enum EnemyStates
        {
            Follow,
            Circle,
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
                    if (Vector2.Distance(transform.position, target.position) < circleRange)
                    {
                        enemyState = EnemyStates.Circle;
                        timer = 0;
                    }
                    break;
                case EnemyStates.Circle:
                    if (Vector2.Distance(transform.position, target.position) < circleRange-2)
                    {
                        base.Update();
                    }
                    CircleMovement(target.position);
                    if (Vector2.Distance(transform.position, target.position) > circleRange)
                    {
                        enemyState = EnemyStates.Follow;
                    }

                    if (timer > 1)
                    {
                        // enemyState = EnemyStates.Shooting;
                        Shoot();
                        timer = 0;
                    }
                    break;
                // case EnemyStates.Shooting:
                //     if (timer > 1)
                //     {
                //         Shoot();
                //     }
                //     break;
            }

            // base.Update();

            

            // if (Vector2.Distance(transform.position, target.position) < attackRange)
            // {
            //     speed = 0;
            //     Shoot();
            // }
            // else
            // {
            //     speed = maxSpeed;
            // }
        }

        private void CircleMovement(Vector2 _direction)
        {
            _direction.x -= transform.position.x;
            _direction.y -= transform.position.y;

            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        public override void Shoot()
        {
            weapon.Shoot(bulletPrefab, this, "Player");
        }

    }
}