using Assets.Scripts.Enems;
using Assets.Scripts;
using UnityEngine;

public class UnitUpgradeButton : MonoBehaviour
{
    [Tooltip("Which unit should be upgraded")]
    [SerializeField] private UnitType unitType;

    [Tooltip("What Stat to Upgrade")]
    [SerializeField] private StatType statType;

    [Header("Upgrade stats Settings")]
    [Tooltip("stat bonus added per upgrade")]
    [SerializeField] private int _statBonus;

    [Header("Upgrade stats Settings")]
    [Tooltip("Percentage to reduce attack delay per upgrade " +
    "(i.e., increase attack speed)." +
    " Accepts decimal values.")]
    [SerializeField] private float _attackDelayReductionPercent;

    [Tooltip("Cost to upgrade unit stat")]
    [SerializeField] private int _statCost;

    [Tooltip("Incremental increase in stat upgrade cost after each upgrade")]
    [SerializeField] private int _statCostInc;


    private void Start()
    {
        _statCost = UpgradeStateManager.Instance.GetStatUpgradeCost(unitType, statType, _statCost);
    }

    public void UpgradeStat()
    {
        var unit = GameDataRepository.Instance.GetFriendlyUnit(unitType);

        if (PlayerCurrency.Instance.HasEnoughMoney(_statCost))
        {
            PlayerCurrency.Instance.SubtractMoney(_statCost);
            ApplyUpgrade(unit);
            _statCost += _statCostInc;
            UpgradeStateManager.Instance.SetStatUpgradeCost(unitType, statType, _statCost);
        }
    }

    private void ApplyUpgrade(UnitData unit)
    {
        switch (statType)
        {
            case StatType.Strength:
                unit._strength += _statBonus;
                break;

            case StatType.Health:
                unit._health += _statBonus;
                break;

            case StatType.Range:
                unit._range += _statBonus;
                break;
            case StatType.AttackSpeed:
                unit._initialAttackDelay *= 1f - (_attackDelayReductionPercent / 100f);
                break;
            default:
                Debug.LogWarning("Unknown stat type: " + statType);
                break;
        }
    }

}
