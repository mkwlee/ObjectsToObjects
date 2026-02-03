using UnityEngine;

namespace SpaceGame
{
    public class PickUp : MonoBehaviour
    {
        [SerializeField] protected GameObject pickUpEffect;
        public virtual void OnPicked()
        {
            GameObject effect = Instantiate(pickUpEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(gameObject);

        }
    }
}