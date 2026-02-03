using System;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceGame
{
    public class EnemyCharger : Enemy
    {
        [Header("Values")]
        [SerializeField] private float chargeRange;
        [SerializeField] private int chargeSpeed;
        [SerializeField] private float chargeDuration;
        [SerializeField] private float attackRange;
        [SerializeField] private float attackTime = 0;
        [SerializeField] private float coolDown;

        [SerializeField] protected GameObject hitEffect;
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
            if (target == null)
                return;

            timer += Time.deltaTime;
            switch (enemyPhase)
            {
                case ChargeState.Follow:
                    base.Update();
                    if (Vector2.Distance(transform.position, target.position) < chargeRange && timer > 0.1f)
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
                    Move(0);
                    if (timer > 0.1)
                    {
                        enemyPhase = ChargeState.Charging;
                        speed = chargeSpeed;
                        timer = 0;
                    }
                    break;
                case ChargeState.Charging:
                    Move(speed);
                    if (Vector2.Distance(transform.position, target.position) < attackRange)
                    {
                        Attack(attackTime);
                        enemyPhase = ChargeState.Cooldown;
                        speed = 0;
                        timer = 0;
                    }

                    if (timer > chargeDuration)
                    {
                        enemyPhase = ChargeState.Cooldown;
                        speed = 0;
                        timer = 0;
                    }
                    break;
                case ChargeState.Cooldown:
                    Move(0);
                    if (timer > coolDown)
                    {
                        enemyPhase = ChargeState.Follow;
                        speed = maxSpeed;
                        timer = 0;
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

        // public void SetMeleeEnemy(float _attackRange, float _attackTime)
        // {
        //     attackRange = _attackRange;
        //     attackTime = _attackTime;
        // }
    }
}