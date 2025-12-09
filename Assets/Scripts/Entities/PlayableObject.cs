using UnityEngine;

namespace SpaceGame
{
    public abstract class PlayableObject : MonoBehaviour, IDamageable
    {
        public Health health;

        public Weapon weapon;

        public abstract void Move(Vector2 direction, Vector2 target);

        public virtual void Move(Vector2 direction) {}

        public virtual void Move(int speed) {}

        public abstract void Shoot();

        public abstract void Attack(float interval);

        public abstract void Die();

        public abstract void GetDamage(int damage);
    }
}