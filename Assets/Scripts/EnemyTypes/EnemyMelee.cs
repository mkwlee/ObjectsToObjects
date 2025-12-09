using UnityEngine;

namespace SpaceGame
{
    public class EnemyMelee : Enemy
    {
        [Header("Values")]
        [SerializeField] private float chargeRange;
        [SerializeField] private float attackRange;
        [SerializeField] private float attackTime = 0;

        private float timer;

        bool isCharging = false;

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
                isCharging = true;
                speed = 0;
                Attack(attackTime);
            }
            else
            {
                speed = maxSpeed;
            }
        }

        public override void Attack(float interval)
        {
            if (timer <= interval)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                target.GetComponent<IDamageable>().GetDamage(weapon.GetDamage());
            }

        }

        public void SetMeleeEnemy(float _attackRange, float _attackTime)
        {
            attackRange = _attackRange;
            attackTime = _attackTime;
        }
    }
}