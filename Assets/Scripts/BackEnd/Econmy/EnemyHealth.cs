using System;
using UnityEngine;


namespace Assets.Scripts.Managers
{
    internal class EnemyHealth
    {
        public static event Action OnEnemyHealthChanged;
        public event Action OnEnemyDied;

        private static EnemyHealth instance;

        // Public property to access the instance
        public static EnemyHealth Instance => instance ??= new EnemyHealth();

        // Private constructor to prevent external instantiation
        private EnemyHealth() { }

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
            OnEnemyHealthChanged?.Invoke();
            return _currentHealth;
        }



        public int SubtractHealth(int amount)
        {
            _currentHealth -= ValidateAmount(Math.Max(0, amount), "subtracting");
            OnEnemyHealthChanged?.Invoke();

            if (_currentHealth <= 0)
            {
                OnEnemyDied?.Invoke(); 
            }

            return _currentHealth;
        }

        public int IncreaseMaxHealth(int amount)
        {
            _maxHealth += ValidateAmount(Math.Max(0, amount), "adding");
            OnEnemyHealthChanged?.Invoke();
            return _maxHealth;
        }

        public int SetMaxHealth(int amount)
        {
            _maxHealth = amount;
            OnEnemyHealthChanged?.Invoke();
            return _maxHealth;
        }

        public void FullHealth()
        {
            _currentHealth = _maxHealth;
            OnEnemyHealthChanged?.Invoke();
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

        public void DisplyHealthInConsole()
        {
            Debug.Log($"Current health amount = {_currentHealth}, Max health amount = {_maxHealth}");
        }

    }
}
