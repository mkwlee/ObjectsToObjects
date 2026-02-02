using UnityEngine;

namespace SpaceGame
{
    public class GunPuckup : PickUp, IDamageable
    {

        [SerializeField] private int powerUpDuration = 10;
        public override void OnPicked()
        {
            
            Player player = GameManager.GetInstance().GetPlayer();
            PlayerInput playerInput = player.GetComponent<PlayerInput>();
            playerInput.SetHoldingShoot();

            GameManager.GetInstance().SetPowerUpTimer(powerUpDuration);
            // player.health.AddHealth(health);

            Debug.Log("Gave player weapon.");
            base.OnPicked();
        }

        public void GetDamage(int damage)
        {
            OnPicked();
        }
    }
}