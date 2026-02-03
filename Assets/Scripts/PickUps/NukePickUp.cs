using SpaceGame;
using UnityEngine;

namespace SpaceGame
{
    public class NukePickup : PickUp, IDamageable
    {
        [SerializeField] private GameObject nukeEffect;
        public override void OnPicked()
        {
            GameManager.GetInstance().ActivatecNuke(nukeEffect);
            base.OnPicked();
        }

        public void GetDamage(int damage)
        {
            OnPicked();
        }
            
    }
}