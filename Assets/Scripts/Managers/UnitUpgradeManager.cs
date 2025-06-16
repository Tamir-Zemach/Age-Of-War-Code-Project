using Assets.Scripts;
using Assets.Scripts.Enems;
using UnityEngine;

public class UnitUpgradeManager : MonoBehaviour
{
    [Header("Strength Upgrade Settings")]
    [Tooltip("Strength bonus added per upgrade")]
    [SerializeField] private int _strengthBonus;

    [Tooltip("Cost to upgrade unit strength")]
    [SerializeField] private int _strengthCost;

    [Tooltip("Incremental increase in strength upgrade cost after each upgrade")]
    [SerializeField] private int _strengthCostInc;

    [Header("Range Upgrade Settings")]
    [Tooltip("Range bonus added per upgrade")]
    [SerializeField] private int _rangeBonus;

    [Tooltip("Cost to upgrade unit range")]
    [SerializeField] private int _rangeCost;

    [Tooltip("Incremental increase in range upgrade cost after each upgrade")]
    [SerializeField] private int _rangeCostInc;

    [Header("Attack Speed Upgrade Settings")]
    [Tooltip("Attack speed bonus added per upgrade")]
    [SerializeField] private int _attackSpeedBonus;

    [Tooltip("Cost to upgrade unit attack speed")]
    [SerializeField] private int _attackSpeedCost;

    [Tooltip("Incremental increase in attack speed upgrade cost after each upgrade")]
    [SerializeField] private int _attackSpeedCostInc;


    public void UpgradeStrength(Unit unit)
    {
        if (PlayerCurrency.Instance.HasEnoughMoney(_strengthCost))
        {
            PlayerCurrency.Instance.SubtractMoney(_strengthCost);
            unit._strength += _strengthBonus;
            _strengthCost += _strengthCostInc;
        }
    }

    public void UpgradeRange(Unit unit)
    {
        if (PlayerCurrency.Instance.HasEnoughMoney(_rangeCost))
        {
            PlayerCurrency.Instance.SubtractMoney(_rangeCost);
            unit._range += _rangeBonus;
            _rangeCost += _rangeCostInc;
        }
    }

    public void UpgradeAttackSpeed(Unit unit)
    {
        if (PlayerCurrency.Instance.HasEnoughMoney(_attackSpeedCost))
        {
            PlayerCurrency.Instance.SubtractMoney(_attackSpeedCost);
            unit._initialAttackDelay += _attackSpeedBonus;
            _attackSpeedCost += _attackSpeedCostInc;
        }
    }
}