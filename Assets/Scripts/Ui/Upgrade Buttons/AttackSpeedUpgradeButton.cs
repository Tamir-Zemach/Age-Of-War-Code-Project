using Assets.Scripts;
using Assets.Scripts.Enems;
using UnityEngine;

public class AttackSpeedUpgradeButton : MonoBehaviour
{
    [SerializeField] private UnitType unitType;

    [Header("Attack Speed Upgrade Settings")]
    [Tooltip("Percentage to reduce attack delay each upgrade (e.g., 10 means 10%)")]
    [SerializeField] private float _attackSpeedPercentReduction;

    [Tooltip("Cost to upgrade unit attack speed")]
    [SerializeField] private int _attackSpeedCost;

    [Tooltip("Incremental increase in attack speed upgrade cost after each upgrade")]
    [SerializeField] private int _attackSpeedCostInc;

    public void UpgradeAttackSpeed()
    {
        var unit = GameManager.ModifiedUnitData[unitType];
        UpgradeReleventStat(unit);
    }

    private void UpgradeReleventStat(Unit unit) =>
    UpgradeStatFloat(ref unit._initialAttackDelay, _attackSpeedPercentReduction, ref _attackSpeedCost, _attackSpeedCostInc);

    private void UpgradeStatFloat(ref float stat, float bonus, ref int cost, int costInc)
    {
        if (PlayerCurrency.Instance.HasEnoughMoney(cost))
        {
            PlayerCurrency.Instance.SubtractMoney(cost);
            stat *= 1f - (bonus / 100f);
            cost += costInc;
        }
    }
}
