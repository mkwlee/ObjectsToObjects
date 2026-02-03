using UnityEngine;


namespace SpaceGame
{
    public class Bullet : MonoBehaviour
    {
        private int damage;
        private int speed;
        private string targetTag;
        [SerializeField] protected GameObject bulletEffect;


        public void SetBullet(int _damage, string _targetTag, int _speed = 30)
        {
            this.damage = _damage;
            this.speed = _speed;
            targetTag = _targetTag;
        }

        private void Update()
        {
            Move();
        }

        void Move()
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }

        void Damage(IDamageable damageable)
        {
            
            damageable.GetDamage(damage);
            GameObject bulletVFX = Instantiate(bulletEffect, transform.position, Quaternion.identity);
            Destroy(bulletVFX, 1f);

            GameManager.GetInstance()?.scoreManager.IncrementScore(5);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {   
            print("HIT = " + other.gameObject.tag);
            if (!other.gameObject.CompareTag(targetTag))
                return;

            IDamageable damageable = other.GetComponent<IDamageable>();
            print("valid = " + (damageable != null));
            if (damageable != null)
            {
                Damage(damageable);
            }
        }
        
    }
}