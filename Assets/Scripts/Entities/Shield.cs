using UnityEngine;
using UnityEngine.Playables;

namespace SpaceGame
{
    public class Shield : DamageableObject
    {

        [SerializeField] public int shieldHealth;
        [SerializeField] private AudioSource shieldHitSound;
        [SerializeField] private GameObject shieldBreakEffect;

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
            shieldHitSound.Play();
            if (health.GetHealth() <= 0)
            {
                GameObject breakVFX = Instantiate(shieldBreakEffect, transform.position, Quaternion.identity);
                Destroy(breakVFX, 2.5f);
                Die();
            }
        }
    }
}