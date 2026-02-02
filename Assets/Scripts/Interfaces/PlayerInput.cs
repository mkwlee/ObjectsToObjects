using UnityEngine;

namespace SpaceGame
{
    public class PlayerInput : MonoBehaviour
    {
        private Player player;
        private float horizontal, vertical;
        private Vector2 lookTarget;

        private bool holdingShoot = false;

        private void Start()
        {
            player = GetComponent<Player>();
            GameManager.GetInstance().powerUpEnded += UnsetHoldingShoot;

        }

        private void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            lookTarget = Input.mousePosition;

            if (holdingShoot == false && Input.GetMouseButtonDown(0))
            {
                player.Shoot();
            }
            else if (holdingShoot == true && Input.GetMouseButton(0))
            {
                player.Shoot();
            }
        }

        void FixedUpdate()
        {
            player.Move(new Vector2(horizontal, vertical), lookTarget);
        }

        public void SetHoldingShoot()
        {
            holdingShoot = true;
        }

        public void UnsetHoldingShoot()
        {
            holdingShoot = false;
        }
    }
}
