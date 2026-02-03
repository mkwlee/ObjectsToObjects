using SpaceGame;
using UnityEngine;

namespace SpaceGame
{
    public class EnemyMelee : Enemy
    {
        [Header("Values")]
        [SerializeField] private float attackRange;
        [SerializeField] private float coolDown;

        [SerializeField] protected GameObject hitEffect;
        private float timer = 2;
        private Vector2 chargeTarget;

        enum MeleeState
        {
            Follow,
            Cooldown
        }
        private MeleeState enemyPhase = MeleeState.Follow;

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            if (target == null)
                return;

            timer += Time.deltaTime;
            switch (enemyPhase)
            {
                case MeleeState.Follow:
                    base.Update();
                    if (Vector2.Distance(transform.position, target.position) < attackRange)
                    {
                        enemyPhase = MeleeState.Cooldown;
                        Attack(0);
                        timer = 0;
                    }
                    break;
                case MeleeState.Cooldown:
                    if (timer > coolDown)
                    {
                        enemyPhase = MeleeState.Follow;
                    }
                    break;
            }
        }

        public override void Attack(float interval)
        {
            GameObject hitVFX = Instantiate(hitEffect, target.position, Quaternion.identity);
            Destroy(hitVFX, 1f);
            target.GetComponent<IDamageable>().GetDamage(weapon.GetDamage());
        }
    }
}
