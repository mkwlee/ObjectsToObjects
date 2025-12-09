using UnityEngine;

namespace SpaceGame
{
    public class PickUp : MonoBehaviour
    {
        public virtual void OnPicked()
        {
            Destroy(gameObject);
        }
    }
}