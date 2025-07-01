﻿using Assets.Scripts.units;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the age-based progression system for both player and enemy factions,
/// applying unit stat upgrades, prefab changes, and UI visual updates.
/// </summary>
public class AgeUpgrade : PersistentMonoBehaviour<AgeUpgrade>
{
    #region Fields & Properties

    public int CurrentPlayerAge { get; private set; } = 1;
    public int CurrentEnemyAge { get; private set; } = 1;

    private List<TurretAndSpecialAttackLevelUpData> _turretAndSpecialAttackData;
    private UnitDeployButton[] _unitDeployButtons;
    private Image[] _deployButtonImages;
    private Image _specialAttckButtonImage;
    private Image _turretButtonImage;

    #endregion

    #region Core Upgrade Logic

    /// <summary>
    /// Applies stat, visual, and reward upgrades to a group of units
    /// based on level-up data and current age.
    /// </summary>
    public void ApplyUpgradesToUnits(List<UnitData> unitDataList, List<UnitLevelUpData> unitLevelUpDataList, int currentAge, bool isFriendly)
    {
        if (NullCheck(unitDataList, unitLevelUpDataList)) return;

        for (int i = 0; i < unitDataList.Count; i++)
        {
            foreach (UnitLevelUpData levelUpData in unitLevelUpDataList)
            {
                if (IsUpgradeRelevant(unitDataList[i], levelUpData, currentAge, isFriendly))
                {
                    UpgradeCoreStats(unitDataList[i], levelUpData);

                    if (isFriendly)
                    {
                        UpdateUnitUISprite(unitDataList[i], levelUpData);
                    }
                    else
                    {
                        UpdateUnitReward(unitDataList[i], levelUpData);
                    }

                    UpgradeStateManager.Instance.SetUnitSprite(unitDataList[i].unitType, levelUpData._unitSprite);
                    UpgradeStateManager.Instance.SetUnitPrefab(unitDataList[i].unitType, levelUpData._unitPrefab);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Evaluates if the given unit and upgrade data match in type, allegiance, and age.
    /// </summary>
    private bool IsUpgradeRelevant(UnitData unit, UnitLevelUpData levelUpData, int currentAge, bool isFriendly)
    {
        return unit.unitType == levelUpData.unitType &&
               unit.isFriendly == isFriendly &&
               levelUpData.isFriendly == isFriendly &&
               (int)levelUpData.ageStage == currentAge;
    }

    public void UpgradeCoreStats(UnitData unit, UnitLevelUpData levelUpData)
    {
        unit._range += levelUpData._range;
        unit._speed += levelUpData._speed;
        unit._health += levelUpData._health;
        unit._strength += levelUpData._strength;
        unit._initialAttackDelay = Mathf.Max(0.1f, unit._initialAttackDelay - levelUpData._initialAttackDelay);
        unit._unitPrefab = levelUpData._unitPrefab;
    }

    public void UpdateUnitReward(UnitData unit, UnitLevelUpData levelUpData)
    {
        unit._moneyWhenKilled += levelUpData._moneyWhenKilled;
    }


    public void AdvanceAge(bool isFriendly)
    {
        if (isFriendly) CurrentPlayerAge++;
        else CurrentEnemyAge++;
    }



    #endregion

    #region UI Handling

    private void UpdateUnitUISprite(UnitData unit, UnitLevelUpData levelUpData)
    {
        _unitDeployButtons = UIObjectFinder.GetAllUnitDeployButtons(out _deployButtonImages);
        for (int i = 0; i < _unitDeployButtons.Length; i++)
        {
            if (_unitDeployButtons[i].UnitType == unit.unitType)
            {
                _deployButtonImages[i].sprite = levelUpData._unitSprite;
                break;
            }
        }
    }

    /// <summary>
    /// Applies turret and special attack upgrades tied to the current player age.
    /// Updates their visual representation in the UI.
    /// </summary>
    public void ApplyTurretAndSpecialAttackUpgrade()
    {
        //TODO: Clean The Code, Add for enemy logic, seperate upgrade and UI update logic
        _turretAndSpecialAttackData = GameDataRepository.Instance.GetturretAndSpecialAttackLevelUpData();

        foreach (var data in _turretAndSpecialAttackData)
        {
            if ((int)data.ageStage == CurrentPlayerAge)
            {
                UpgradeStateManager.Instance.UpgradeSpecialAttack(data.specialAttackType);
                UpgradeStateManager.Instance.SetSpecialAttackSprite(data.specialAttackType, data._SpecialAttackSprite);
                UpgradeStateManager.Instance.SetTurretSprite(data._TurretSprite);

                if (_specialAttckButtonImage != null && data._SpecialAttackSprite != null)
                {
                    _specialAttckButtonImage.sprite = data._SpecialAttackSprite;
                }
                if (_turretButtonImage != null && data._TurretSprite != null)
                {
                    _turretButtonImage.sprite = data._TurretSprite;
                }

                break;
            }
        }
    }

    #endregion


    private bool NullCheck(List<UnitData> unitDataList, List<UnitLevelUpData> unitLevelUpDataList)
    {
        if (unitDataList == null || unitLevelUpDataList == null)
        {
            Debug.LogWarning($"Upgrade failed: UnitData is {unitDataList?.Count} items, LevelUpData is {unitLevelUpDataList?.Count} items");
            return true;
        }

        return false;
    }

}