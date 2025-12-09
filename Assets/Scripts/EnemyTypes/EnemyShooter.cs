using UnityEngine;

namespace SpaceGame
{
    public class EnemyShooter : Enemy
    {
        [Header("Values")]
        [SerializeField] private float attackRange;
        [SerializeField] private float attackTime;

        [SerializeField] private Bullet bulletPrefab;
        
        private float timer;

        protected override void Start()
        {
            base.Start();
        }


        protected override void Update()
        {
            base.Update();

            if (target == null)
                return;

            if (Vector2.Distance(transform.position, target.position) < attackRange)
            {
                speed = 0;
                Shoot();
            }
            else
            {
                speed = maxSpeed;
            }
        }

        public override void Shoot()
        {
            if (timer <= attackTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                // target.GetComponent<IDamageable>().GetDamage(weapon.GetDamage());
                weapon.Shoot(bulletPrefab, this, "Player");
            }

            // weapon.Shoot(bulletPrefab, this, "Player");
        }

    }
}