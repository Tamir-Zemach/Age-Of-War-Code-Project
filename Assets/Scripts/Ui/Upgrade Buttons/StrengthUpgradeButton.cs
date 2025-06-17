using Assets.Scripts;
using Assets.Scripts.Enems;
using UnityEngine;

public class StrengthUpgradeButton : MonoBehaviour
{
    [SerializeField] private UnitType unitType;

    [Header("Strength Upgrade Settings")]
    [Tooltip("Strength bonus added per upgrade")]
    [SerializeField] private int _strengthBonus;

    [Tooltip("Cost to upgrade unit strength")]
    [SerializeField] private int _strengthCost;

    [Tooltip("Incremental increase in strength upgrade cost after each upgrade")]
    [SerializeField] private int _strengthCostInc;

    public void UpgradeStrength()
    {
        var unit = GameManager.ModifiedUnitData[unitType];
        UpgradeReleventStat(unit);
    }
    private void UpgradeReleventStat(Unit unit) =>
    UpgradeStat(ref unit._strength, _strengthBonus, ref _strengthCost, _strengthCostInc);

    private void UpgradeStat(ref int stat, int bonus, ref int cost, int costInc)
    {
        if (PlayerCurrency.Instance.HasEnoughMoney(cost))
        {
            PlayerCurrency.Instance.SubtractMoney(cost);
            stat += bonus;
            cost += costInc;
        }
    }
}
