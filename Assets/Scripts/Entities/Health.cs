using System;
using UnityEngine;

namespace SpaceGame
{
    public class Health
    {
        private int currentHealth;
        private int maxHealth;
        private int healthRegenRate;

        public Action<int> OnHealthUpdate;

        public int GetHealth()
        {
            return currentHealth;
        }

        public void SetHealth(int value)
        {
            if (value > maxHealth || value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"Valid range for health is between 0 and {maxHealth}.");
            }

            currentHealth = value;
        }

        public Health(int _maxHealth, int _healthRegenRate, int _currentHealth = 100)
        {
            currentHealth = _currentHealth;
            maxHealth = _maxHealth;
            healthRegenRate = _healthRegenRate;

            OnHealthUpdate?.Invoke(currentHealth);
        }

        public Health(int _maxHealth)
        {
            maxHealth = _maxHealth;
        }


        public Health() { }


        public void AddHealth(int value)
        {
            currentHealth = Mathf.Min(maxHealth, currentHealth + value);
            OnHealthUpdate?.Invoke(currentHealth);
        }

        public void DeductHealth(int value)
        {
            currentHealth = Mathf.Max(0, currentHealth - value);
            OnHealthUpdate?.Invoke(currentHealth);
        }
    }
}