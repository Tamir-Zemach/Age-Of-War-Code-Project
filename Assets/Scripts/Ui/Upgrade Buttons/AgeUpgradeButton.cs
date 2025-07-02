using Assets.Scripts;
using Assets.Scripts.Data;
using Assets.Scripts.Enems;
using Assets.Scripts.turrets;
using Assets.Scripts.units;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AgeUpgradeButton : MonoBehaviour
{
    [SerializeField] private int _ageUpgradeCost;

    public void UpdateAge()
    {
        if (PlayerCurrency.Instance.HasEnoughMoney(_ageUpgradeCost))
        {
            PlayerCurrency.Instance.SubtractMoney(_ageUpgradeCost);
            GameManager.Instance.UpgradePlayerAge();

        }
    }




}
