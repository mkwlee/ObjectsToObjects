using System;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceGame
{
    public class EnemyMelee : Enemy
    {
        [Header("Values")]
        [SerializeField] private float chargeRange;
        [SerializeField] private float attackRange;
        [SerializeField] private float attackTime = 0;

        private float timer = 2;
        private Vector2 chargeTarget;

        enum ChargeState
        {
            Follow,
            Preparing,
            Charging,
            Cooldown
        }
        private ChargeState enemyPhase = ChargeState.Follow;

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            Debug.Log(enemyPhase);
            Debug.Log(speed);
            if (target == null)
                return;

            timer += Time.deltaTime;
            switch (enemyPhase)
            {
                case ChargeState.Follow:
                    base.Update();
                    if (Vector2.Distance(transform.position, target.position) < chargeRange && timer > 3)
                    {
                        enemyPhase = ChargeState.Preparing;
                        speed = 0;
                        timer = 0;
                        chargeTarget = target.position;
                        Vector2 dir = (chargeTarget - (Vector2)transform.position).normalized;
                        transform.up = dir;
                    }
                    break;
                case ChargeState.Preparing:
                    // Move(speed);
                    if (timer > 1)
                    {
                        enemyPhase = ChargeState.Charging;
                        speed = maxSpeed*5;
                        timer = 0;
                    }
                    break;
                case ChargeState.Charging:
                    Move(speed);
                    if (Vector2.Distance(transform.position, target.position) < attackRange)
                    {
                        Attack(0);
                        enemyPhase = ChargeState.Cooldown;
                        speed = 0;
                        timer = 0;
                    }

                    if (timer > 2)
                    {
                        enemyPhase = ChargeState.Cooldown;
                        speed = 0;
                        timer = 0;
                    }
                    break;
                case ChargeState.Cooldown:
                    // Move(speed);
                    if (timer > 1)
                    {
                        enemyPhase = ChargeState.Follow;
                        speed = maxSpeed;
                        timer = 0;
                    }
                    break;
            }
            
            // timer += Time.deltaTime;
            // if (isCharging)
            // {
            //     Move(chargeTarget);

            //     if (Vector2.Distance(transform.position, target.position) < attackRange)
            //     {
            //         Attack(0);
            //         isCharging = false;
            //     }
            //     if (timer > 0.5)
            //     {
            //         speed = maxSpeed*5;
            //     }

            //     if (Vector2.Distance(transform.position, chargeTarget) < attackRange)
            //     {
            //         isCharging = false;
            //     }
            // }
            // else
            // {
            //     base.Update();
            //     if (Vector2.Distance(transform.position, target.position) < chargeRange && timer > 3)
            //     {
            //         isCharging = true;
            //         speed = 0;
            //         timer = 0;
            //         chargeTarget = target.position;
            //         // Attack(attackTime);
            //     }
            //     else
            //     {
            //         speed = maxSpeed;
            //     }
            // }  
        }

        public override void Attack(float interval)
        {
            target.GetComponent<IDamageable>().GetDamage(weapon.GetDamage());
        }

        public void SetMeleeEnemy(float _attackRange, float _attackTime)
        {
            attackRange = _attackRange;
            attackTime = _attackTime;
        }
    }
}