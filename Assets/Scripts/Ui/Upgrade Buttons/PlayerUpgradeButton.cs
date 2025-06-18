using Assets.Scripts.Enems;
using Assets.Scripts;
using UnityEngine;
using Assets.Scripts.Managers;
using System.Collections.Generic;

public class PlayerUpgradeButton : MonoBehaviour
{
    [Tooltip("Specifies the type of upgrade to apply.")]
    [SerializeField] private UpgradeType upgradeType;

    [Header("Upgrade stats Settings")]

    [Tooltip("Bonus amount added to the stat with each upgrade.")]
    [SerializeField] private int _statBonus;

    [Tooltip("Cost to upgrade unit stat")]
    [SerializeField] private int _statUpgradeCost;

    [Tooltip("Incremental increase in stat upgrade cost after each upgrade")]
    [SerializeField] private int _statCostInc;

    private List<Unit> _allFriendlyUnits;
    private List<Unit> _allEnemyUnits;

    public void UpgradeStat()
    {

        if (PlayerCurrency.Instance.HasEnoughMoney(_statUpgradeCost))
        {
            PlayerCurrency.Instance.SubtractMoney(_statUpgradeCost);
            ApplyUpgrade();
            _statUpgradeCost += _statCostInc;
        }
    }

    private void ApplyUpgrade()
    {
        switch (upgradeType)
        {
            case UpgradeType.MaxHealthIncrease:
                PlayerHealth.Instance.IncreaseMaxHealth(_statBonus);
                break;

            case UpgradeType.UnitsCosts:
                _allFriendlyUnits = GameManager.Instance.GetAllInstantiatedUnits();
                DecreaseCostToAllFrienlyUnits();
                break;

            case UpgradeType.EnemyMoneyIncrease:
                _allEnemyUnits = EnemySpawner.Instance.GetAllInstantiatedUnits();
                IncreaseMoneyGainToAllEnemyUnits();
                break;
            default:
                Debug.LogWarning("Unknown upgrade type: " + upgradeType);
                break;
        }
    }


    private void DecreaseCostToAllFrienlyUnits()
    {
        foreach (Unit unit in _allFriendlyUnits)
        {
            unit._cost -= _statBonus;
        }
    }
    private void IncreaseMoneyGainToAllEnemyUnits()
    {
        foreach (Unit unit in _allEnemyUnits)
        {
            unit._moneyWhenKilled += _statBonus;
        }
    }

}
