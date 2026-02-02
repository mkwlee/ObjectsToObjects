using UnityEngine;

namespace SpaceGame
{
    public abstract class DamageableObject : MonoBehaviour, IDamageable
    {
        public Health health;

        public abstract void Die();
        public abstract void GetDamage(int damage);
    }
}