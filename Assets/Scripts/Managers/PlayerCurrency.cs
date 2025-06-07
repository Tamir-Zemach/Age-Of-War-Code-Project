using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerCurrency
    {

        private static PlayerCurrency instance;

        // Public property to access the instance
        public static PlayerCurrency Instance => instance ??= new PlayerCurrency();

        // Private constructor to prevent external instantiation
        private PlayerCurrency() { }

        private int _money = 100;

        public int Money
        {
            get => _money;
            set => _money = Math.Max(0, value);
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

        public int AddMoney(int amount) => Money += ValidateAmount(amount, "adding");

        public int SubtractMoney(int amount) => Money -= ValidateAmount(amount, "subtracting");

        public void DisplyMoneyInConsole()
        {
            Debug.Log($"current money amount = {_money}");
        }

    }
}
