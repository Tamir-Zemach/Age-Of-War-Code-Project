using Assets.Scripts;
using Assets.Scripts.Enems;
using Assets.Scripts.units;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AgeUpgradeButton : MonoBehaviour
{
    [SerializeField] private int _ageUpgradeCost;
    private List<UnitData> _friendlyUnitData;
    private List<UnitLevelUpData> _unitLevelUpData;

    private void Start()
    {
        _friendlyUnitData = GameDataRepository.Instance.GetAllFriendlyUnits();
        _unitLevelUpData = GameDataRepository.Instance.GetUnitLevelUpData();
    }

    public void UpdateAge()
    {
        if (PlayerCurrency.Instance.HasEnoughMoney(_ageUpgradeCost))
        {
            PlayerCurrency.Instance.SubtractMoney(_ageUpgradeCost);
            AgeUpgrade.Instance.AdvanceAge(isFriendly: true);
            AgeUpgrade.Instance.ApplyUpgradesToUnits(_friendlyUnitData, _unitLevelUpData, AgeUpgrade.Instance.CurrentPlayerAge, isFriendly: true);
            AgeUpgrade.Instance.ApplySpecialAttackUpgrade();
            AgeUpgrade.Instance.ApplyTurretUpgrade();
        }
    }




}
