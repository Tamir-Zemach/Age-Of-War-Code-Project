using System;
using UnityEngine;


namespace Assets.Scripts.Managers
{
    internal class PlayerHealth
    {
        public static event Action OnHealthChanged;

        private static PlayerHealth instance;

        // Public property to access the instance
        public static PlayerHealth Instance => instance ??= new PlayerHealth();

        // Private constructor to prevent external instantiation
        private PlayerHealth() { }

        private int _currentHealth = 1;
        private int _maxHealth = 2;
        public int CurrentHealth => _currentHealth; // Read-only property
        public int MaxHealth => _maxHealth; // Read-only property

        public int AddHealth(int amount)
        {
            _currentHealth += ValidateAmount(Math.Max(0, amount), "adding");
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
                return _currentHealth;
            }
            OnHealthChanged?.Invoke();
            return _currentHealth;
        }

        public int SubtractHealth(int amount)
        {
            _currentHealth -= ValidateAmount(Math.Max(0, amount), "subtracting");
            OnHealthChanged?.Invoke();
            return _currentHealth;
        }
        public int IncreaseMaxHealth(int amount)
        {
            _maxHealth += ValidateAmount(Math.Max(0, amount), "adding");
            return _maxHealth;
        }

        public int SetMaxHealth(int amount)
        {
            _maxHealth = amount;
            OnHealthChanged?.Invoke();
            return _maxHealth;
        }

        public void FullHealth()
        {
            _currentHealth = _maxHealth;
            OnHealthChanged?.Invoke();
        }

        private int ValidateAmount(int amount, string operation)
        {
            if (amount < 0)
            {
                Debug.LogWarning($"{amount} is negative. Please use a positive amount for {operation}.");
                return 0;
            }
            return amount;
        }

        public bool PlayerDied()
        {
            return _currentHealth <= 0; 
        }
        public void DisplyHealthInConsole()
        {
            Debug.Log($"Current health amount = {_currentHealth}, Max health amount = {_maxHealth}");
        }

    }
}
