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
        [SerializeField] private Bullet machineGunBulletPrefab;
        public bool isShootingMachineGun = false;
        [SerializeField] private AudioSource hurtAudio;
        [SerializeField] private GameObject deathEffect;
        private Camera cam;

        public Action OnDeath;

        private Rigidbody2D playerRB;

        public Shield currentShield { get; private set; }

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
            if(isShootingMachineGun)
            {
                weapon.Shoot(machineGunBulletPrefab, this, "Damageable");
            }
            else
            {
                weapon.Shoot(bulletPrefab, this, "Damageable");
            }
            GameManager.GetInstance().cameraManager.ShakeCamera(0.5f, 0.1f, 1, 1);
        }

        public override void Attack(float interval)
        {
            throw new System.NotImplementedException();
        }

        public override void Die()
        {
            Debug.Log($"Player has died.");
            GameObject deathVFX = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(deathVFX, 3f);
            OnDeath?.Invoke();
            Destroy(gameObject);
        }

        public override void GetDamage(int damage)
        {
            Debug.Log($"Player took {damage} damage.");
            health.DeductHealth(damage);
            // if (!hurtAudio.isPlaying)
            // {
            hurtAudio.Play();
            // }
                
            if (health.GetHealth() <= 0)
            {
                GameManager.GetInstance().cameraManager.ShakeCamera(1f, 1f, 4, 10);
                Die();
            } else
            {
                GameManager.GetInstance().cameraManager.ShakeCamera(0.5f, 0.5f, 3, 4);
            }
        }

        public void SetShield(Shield shield)
        {
            currentShield = shield;
        }

    }
}