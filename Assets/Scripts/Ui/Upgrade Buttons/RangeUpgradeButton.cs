using Assets.Scripts;
using Assets.Scripts.Enems;
using UnityEngine;

public class RangeUpgradeButton : MonoBehaviour
{
    [SerializeField] private UnitType unitType;

    [Header("Range Upgrade Settings")]
    [Tooltip("Range bonus added per upgrade")]
    [SerializeField] private int _rangeBonus;

    [Tooltip("Cost to upgrade unit range")]
    [SerializeField] private int _rangeCost;

    [Tooltip("Incremental increase in range upgrade cost after each upgrade")]
    [SerializeField] private int _rangeCostInc;

    public void UpgradeRange()
    {
        var unit = GameManager.ModifiedUnitData[unitType];
        UpgradeReleventStat(unit);
    }

    private void UpgradeReleventStat(Unit unit) =>
    UpgradeStat(ref unit._range, _rangeBonus, ref _rangeCost, _rangeCostInc);

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
