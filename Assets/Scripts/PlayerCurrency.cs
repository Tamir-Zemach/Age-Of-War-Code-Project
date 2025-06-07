using System;
using UnityEngine;

namespace Assets.Scripts
{
    internal class PlayerCurrency 
    {
        private int _money;

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

    }
}
