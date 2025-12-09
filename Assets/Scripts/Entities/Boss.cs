using UnityEngine;

namespace SpaceGame
{
    public class Boss : Enemy
    {
        public override void Attack(float interval)
        {
            Debug.Log("Boss unleashes a powerful attack!");
        }
    }
}