using UnityEngine;

namespace SpaceGame
{
    public class EnemyExploder : Enemy
    {

        public float fuseTime { get; private set; }

        public override void Attack(float interval)
        {
            Debug.Log($"Enemy attacks by exploding!");
        }
    }
}
