using SpaceGame;
using UnityEngine;

namespace SpaceGame
{
    public class NukePickup : PickUp, IDamageable
    {
        public override void OnPicked()
        {
            GameManager.GetInstance().ActivatecNuke();
            base.OnPicked();
        }

        public void GetDamage(int damage)
        {
            OnPicked();
        }
            
    }
}