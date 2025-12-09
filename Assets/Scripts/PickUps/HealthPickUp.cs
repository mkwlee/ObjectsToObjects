using SpaceGame;
using UnityEngine;

namespace SpaceGame
{
    public class HealthPickUp : PickUp, IDamageable
    {
        [SerializeField] private int healthMin = 1;
        [SerializeField] private int healthMax = 5;


        public override void OnPicked()
        {
            base.OnPicked();
            int health = Random.Range(healthMin, healthMax);

            Debug.Log("Added health to player");
        }

        public void GetDamage(int damage)
        {
            OnPicked();
        }
            
    }
}