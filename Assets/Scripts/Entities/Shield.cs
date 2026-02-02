using UnityEngine;
using UnityEngine.Playables;

namespace SpaceGame
{
    public class Shield : DamageableObject
    {

        [SerializeField] public int shieldHealth;

        protected void Start()
        {
            health = new Health(shieldHealth, 0, shieldHealth); // Initialize health with 3 hit points
        }

        public override void Die()
        {
            Destroy(gameObject);
        }
        public override void GetDamage(int damage)
        {
            health.DeductHealth(damage);
            if (health.GetHealth() <= 0)
            {
                Die();
            }
        }
    }
}