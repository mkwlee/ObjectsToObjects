using DG.Tweening;
using UnityEngine;

namespace SpaceGame
{
    public class PlayerInput : MonoBehaviour
    {
        private Player player;
        private float horizontal, vertical;
        private Vector2 lookTarget;

        private bool holdingShoot = false;
        private float shootDelay = 0f;
        private const float shootInterval = 0.1f;

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

            shootDelay += Time.deltaTime;
            if (holdingShoot == false && Input.GetMouseButtonDown(0))
            {
                if (shootDelay < shootInterval) return;
                shootDelay = 0f;
                player.Shoot();
            }
            else if (holdingShoot == true && Input.GetMouseButton(0))
            {
                if (shootDelay < shootInterval) return;
                shootDelay = 0f;
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
            player.isShootingMachineGun = true;
            player.gameObject.GetComponent<SpriteRenderer>().DOColor(new Color32(138, 0, 196, 255), 0.2f);
        }

        public void UnsetHoldingShoot()
        {   
            holdingShoot = false;
            player.isShootingMachineGun = false;
            player.gameObject.GetComponent<SpriteRenderer>().DOColor(new Color32(9, 0, 255, 255), 0.2f);
        }
    }
}
