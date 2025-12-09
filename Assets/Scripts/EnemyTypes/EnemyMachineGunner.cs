using UnityEngine;

namespace SpaceGame
{
    public class EnemyMachineGunner : Enemy
    {

        private float shootingRate;
        public float shootingCoolDown;


        public override void Shoot()
        {
            Debug.Log("Enemy shoots rapid fire!!!");
        }
    }
}