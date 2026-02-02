using UnityEngine;

namespace SpaceGame
{
    public class EnemyExploder : Enemy
    {

        [SerializeField] private float fuseTime;
        [SerializeField] private float explosionRange;

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
                    if (Vector2.Distance(transform.position, target.position) < explosionRange / 2)
                    {
                        enemyState = EnemyStates.Fuse;
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

                    if (Vector2.Distance(transform.position, target.position) > explosionRange)
                    {
                        enemyState = EnemyStates.Follow;
                        speed = maxSpeed;
                    }
                    break;
            }
        }

        public override void Attack(float interval)
        {
            target.GetComponent<IDamageable>().GetDamage(weapon.GetDamage());
            Destroy(gameObject);
        }
    }
}
