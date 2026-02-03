using SpaceGame;
using UnityEngine;
using UnityEngine.VFX;

namespace SpaceGame
{
    public class Enemy : PlayableObject
    {

        [Header("Stats")]
        [SerializeField] protected int maxHealth;
        [SerializeField] protected int maxSpeed;

        [Header("Weapon")]
        [SerializeField] protected string weaponName;
        [SerializeField] protected int weaponDamage;
        [SerializeField] protected int weaponSpeed;

        [Header("MISC")]
        [SerializeField] protected GameObject deathEffect;
        [SerializeField] protected AudioSource hitSound;
        protected Transform target;
        protected int speed;
        [SerializeField] private EnemyType enemyType;


        protected virtual void Start()
        {
            target = GameManager.GetInstance().GetPlayer().gameObject.transform;
            health = new Health(maxHealth, 0, maxHealth);
            speed = maxSpeed;
            weapon = new Weapon(weaponName, weaponDamage, weaponSpeed);
        }
        
        protected virtual void Update()
        {
            if (target != null)
            {

                Move(target.position);
            }
            else
            {
                Move(speed);
            }

        }

        public override void Move(Vector2 _direction, Vector2 _target){
        }

        public override void Move(int _speed)
        {
            // GetComponent<Rigidbody2D>().linearVelocity = (Vector2)transform.up * speed * Time.deltaTime;
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        public override void Move(Vector2 _direction)
        {
            // Vector2 direction = _direction - (Vector2)transform.position;

            _direction.x -= transform.position.x;
            _direction.y -= transform.position.y;

            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            transform.Translate(Vector2.up * speed * Time.deltaTime);

            
            // float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            // transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            // GetComponent<Rigidbody2D>().linearVelocity = direction * speed * Time.deltaTime;
        }

        public override void Shoot()
        {
            Debug.Log($"Enemy is shooting");
        }

        public override void Attack(float interval)
        {
            Debug.Log("Enemy attacks!");
        }

        public override void Die()
        {
            GameObject deathVFX = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(deathVFX, 2.5f);
            if (enemyType == EnemyType.Grenader || enemyType == EnemyType.MachineGunner || enemyType == EnemyType.Charger)
            {
                GameManager.GetInstance().scoreManager.IncrementScore(25);
            }
            else
            {
                GameManager.GetInstance().scoreManager.IncrementScore(15);
            }
            
            GameManager.GetInstance().NotifyDeath(this);
            Destroy(gameObject);
        }

        public override void GetDamage(int damage)
        {
            health.DeductHealth(damage);
            hitSound.Play();
            if (health.GetHealth() <= 0)
            {
                GameManager.GetInstance().cameraManager.ShakeCamera(0.5f, 0.1f, 3, 3);
                Die();
            }
            {
                GameManager.GetInstance().cameraManager.ShakeCamera(0.5f, 0.1f, 2, 2);
            }
        }
        
        public void SetEnemyType(EnemyType type)
        {
            enemyType = type;
        }
    }
}
