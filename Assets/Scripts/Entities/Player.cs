using UnityEngine;
using System;

namespace SpaceGame
{
    public class Player : PlayableObject
    {
        [SerializeField] private float speed;
        [SerializeField] private int weaponDamage = 1;
        [SerializeField] private int bulletSpeed = 10;
        [SerializeField] private Bullet bulletPrefab;
        private Camera cam;

        public Action OnDeath;

        private Rigidbody2D playerRB;

        private void Awake()
        {
            health = new Health(200, 1);
        }

        private void Start()
        {
            cam = Camera.main;
            playerRB = GetComponent<Rigidbody2D>();
            weapon = new Weapon("Player Weapon", weaponDamage, bulletSpeed);
            // nickname = "Player";
        }

        public override void Move(Vector2 direction, Vector2 target)
        {
            playerRB.linearVelocity = direction * speed * Time.deltaTime;

            var playerScreenPos = cam.WorldToScreenPoint(transform.position);
            target.x -= playerScreenPos.x;
            target.y -= playerScreenPos.y;

            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        }

        public override void Shoot()
        {
            weapon.Shoot(bulletPrefab, this, "Damageable");
        }

        public override void Attack(float interval)
        {
            throw new System.NotImplementedException();
        }

        public override void Die()
        {
            Debug.Log($"Player has died.");
            OnDeath?.Invoke();
            Destroy(gameObject);
        }

        public override void GetDamage(int damage)
        {
            Debug.Log($"Player took {damage} damage.");
            health.DeductHealth(damage);
            if (health.GetHealth() <= 0)
            {
                Die();
            }
        }

    }
}