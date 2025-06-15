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

        private int _health = 0;
        public int Health => _health; // Read-only property

        public int AddHealth(int amount)
        {
            _health += ValidateAmount(Math.Max(0, amount), "adding");
            OnHealthChanged?.Invoke();
            return _health;
        }

        public int SubtractHealth(int amount)
        {
            _health -= ValidateAmount(Math.Max(0, amount), "subtracting");
            OnHealthChanged?.Invoke();
            return _health;
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
            return _health <= 0; 
        }
        public void DisplyHealthInConsole()
        {
            Debug.Log($"current health amount = {_health}");
        }

    }
}
