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
            int health = Random.Range(healthMin, healthMax);
            
            Player player = GameManager.GetInstance().GetPlayer();
            player.health.AddHealth(health);

            Debug.Log("Added health to player");
            base.OnPicked();
        }

        public void GetDamage(int damage)
        {
            OnPicked();
        }
            
    }
}