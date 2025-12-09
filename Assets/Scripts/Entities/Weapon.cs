using UnityEngine;

namespace SpaceGame
{
    public class Weapon
    {
        private string name;
        private int damage;
        private int bulletSpeed;


        public Weapon(string _name, int _damage, int _bulletSpeed)
        {
            name = _name;
            damage = _damage;
            bulletSpeed = _bulletSpeed;
        }

        public Weapon() { }

        public void Shoot(Bullet _bullet, PlayableObject _player, string _targetTag, int _timeToDie = 5)
        {
            Bullet tempBullet = GameObject.Instantiate(_bullet, _player.transform.position, _player.transform.rotation);
            tempBullet.SetBullet(damage, _targetTag, bulletSpeed);
            GameObject.Destroy(tempBullet.gameObject, _timeToDie);
        }

        public int GetDamage() { return damage; }
    }
}