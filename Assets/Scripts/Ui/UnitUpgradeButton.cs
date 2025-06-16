
using Assets.Scripts.Enems;
using UnityEngine;

public class UnitUpgradeButton : MonoBehaviour
{
    [SerializeField] private UnitType unitType; 

    public void UpgradeStrength()
    {
        var unit = GameManager.ModifiedUnitData[unitType];
        UnitUpgradeManager.Instance.UpgradeStrength(unit);
    }

    public void UpgradeRange()
    {
        var unit = GameManager.ModifiedUnitData[unitType];
        UnitUpgradeManager.Instance.UpgradeRange(unit);
    }

    public void UpgradeAttackSpeed()
    {
        var unit = GameManager.ModifiedUnitData[unitType];
        UnitUpgradeManager.Instance.UpgradeAttackSpeed(unit);
    }
}