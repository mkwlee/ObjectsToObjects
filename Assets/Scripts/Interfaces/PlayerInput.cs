using UnityEngine;

namespace SpaceGame
{
    public class PlayerInput : MonoBehaviour
    {
        private Player player;
        private float horizontal, vertical;
        private Vector2 lookTarget;

        private void Start()
        {
            player = GetComponent<Player>();
        }

        private void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            lookTarget = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                player.Shoot();
            }
        }

        void FixedUpdate()
        {
            player.Move(new Vector2(horizontal, vertical), lookTarget);
        }
    }
}
