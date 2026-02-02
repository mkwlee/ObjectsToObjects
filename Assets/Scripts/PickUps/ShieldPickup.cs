using SpaceGame;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceGame
{
    public class ShieldPickup : PickUp, IDamageable
    {
        [SerializeField] private Shield shieldPrefab;
        public override void OnPicked()
        {
            Player player = GameManager.GetInstance().GetPlayer();
            if (player.currentShield == null)
            {
                Shield newShield = Instantiate(shieldPrefab, player.transform);
                player.SetShield(newShield);
            } else
            {
                player.currentShield.health.SetHealth(player.currentShield.shieldHealth);
            }
            base.OnPicked();
        }

        public void GetDamage(int damage)
        {
            OnPicked();
        }
    }
}