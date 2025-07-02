using Assets.Scripts.Data;
using Assets.Scripts.Enems;
using Assets.Scripts.InterFaces;
using Assets.Scripts.turrets;
using Assets.Scripts.Ui.TurretButton;
using Assets.Scripts.units;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages the age-based progression system for both player and enemy factions,
/// applying unit stat upgrades, prefab changes, and UI visual updates.
/// </summary>
public class AgeUpgrade : SceneAwareMonoBehaviour<AgeUpgrade>
{
    #region Fields & Properties

    public int CurrentPlayerAge { get; private set; } = 1;
    public int CurrentEnemyAge { get; private set; } = 1;


    private UnitDeployButton[] _unitDeployButtons;
    private SpecialAttackButton _specialAttackButton;
    private TurretButton _turretButton;


    #endregion

    protected override void InitializeOnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        _specialAttackButton = UIObjectFinder.GetSpecialAttackButton();
    }


    #region Core Upgrade Logic




    /// <summary>
    /// Loops through all unit data and applies matching upgrades based on type, age, and faction.
    /// For friendly units, it also updates the UI and registered sprites/prefabs.
    /// Enemy units receive stat boosts and updated reward values.
    /// </summary>
    public void ApplyUpgradesToUnits(List<UnitData> unitDataList, List<UnitLevelUpData> unitLevelUpDataList, int currentAge, bool isFriendly)
    {
        if (NullChecks.DataListNullCheck(unitDataList, unitLevelUpDataList)) return;

        for (int i = 0; i < unitDataList.Count; i++)
        {
            foreach (UnitLevelUpData levelUpData in unitLevelUpDataList)
            {
                if (IsUpgradeRelevant<UnitData, UnitLevelUpData, UnitType>(unitDataList[i],
                    levelUpData,
                    currentAge,
                    isFriendly))
                {
                    UpgradeCoreStats(unitDataList[i], levelUpData);

                    UpdateDataPrefab<UnitData, UnitLevelUpData, UnitType>(unitDataList[i], levelUpData);

                    if (isFriendly)
                    {
                        UpdateUnitUISprite(unitDataList[i], levelUpData);
                    }
                    else
                    {
                        UpdateUnitReward(unitDataList[i], levelUpData);
                    }

                    UpgradeStateManager.Instance.SetUnitPrefab(unitDataList[i].Type, levelUpData.Prefab);
                    break;
                }
            }
        }
    }


    /// <summary>
    /// Applies an upgrade to a special attack if it matches the faction and current age level.
    /// Updates both the prefab and, if friendly, the UI button’s sprite to reflect the new upgrade.
    /// </summary>
    public void ApplySpecialAttackUpgrade(SpecialAttackData specialAttackData,
                      SpecialAttackLevelUpData specialAttackLevelUpData,
                      int currentAge,
                      bool isFriendly)
    {
        if (NullChecks.DataNullCheck(specialAttackData, specialAttackLevelUpData)) return;


        if (IsUpgradeRelevant<SpecialAttackData, SpecialAttackLevelUpData, SpecialAttackType>(specialAttackData, specialAttackLevelUpData, currentAge, isFriendly))
        {
            UpdateDataPrefab<SpecialAttackData, SpecialAttackLevelUpData, SpecialAttackType>(specialAttackData, specialAttackLevelUpData);

            if (isFriendly)
            {
                UpdateSpecialAttackSprite(specialAttackData, specialAttackLevelUpData);
            }
            UpgradeStateManager.Instance.SetSpecialAttackPrefab(specialAttackData.Type, specialAttackLevelUpData.Prefab);
        }
    }

    public void ApplyTurretUpgrade(TurretData turretData,
                      TurretLevelUpData turretLevelUpData,
                      int currentAge,
                      bool isFriendly)
    {
        if (NullChecks.DataNullCheck(turretData, turretLevelUpData)) return;

        if (IsUpgradeRelevant<TurretData, TurretLevelUpData, TurretType>(turretData, turretLevelUpData, currentAge, isFriendly))
        {
            UpdateDataPrefab<TurretData, TurretLevelUpData, TurretType>(turretData, turretLevelUpData);

            if (isFriendly)
            {
                //TODO: implement the sprite update logic 

            }
            UpgradeStateManager.Instance.SetTurretPrefab(turretData.Type, turretLevelUpData.Prefab);

        }
    }




    /// <summary>
    /// Determines whether a level-up upgrade is valid for a specific data object (like a unit or special attack),
    /// based on matching type, faction alignment, and age stage requirements.
    /// </summary>
    /// <typeparam name="TData">The current data class (e.g. UnitData, SpecialAttackData) implementing IUpgradable&lt;TType&gt;.</typeparam>
    /// <typeparam name="TlevelUpData">The upgrade data class (e.g. UnitLevelUpData, SpecialAttackLevelUpData) implementing ILevelUpData&lt;TType&gt;.</typeparam>
    /// <typeparam name="TType">The shared type used to identify the object (e.g. UnitType, SpecialAttackType).</typeparam>
    /// <param name="data">The current data instance that may receive an upgrade.</param>
    /// <param name="levelUpData">The level-up data containing upgrade values and criteria.</param>
    /// <param name="currentAge">The age level currently reached in the game, used to determine if the upgrade is unlocked.</param>
    /// <param name="isFriendly">Indicates whether the upgrade applies to the friendly side or enemy side.</param>
    /// <returns>True if the upgrade matches the object's type, side (friendly/enemy), and current age stage; otherwise, false.</returns>
    private bool IsUpgradeRelevant<TData, TlevelUpData, TType>(
        TData data,
        TlevelUpData levelUpData,
        int currentAge,
        bool isFriendly)
        where TData : IUpgradable<TType>
        where TlevelUpData : IUpgradable<TType>
    {
        return data.Type.Equals(levelUpData.Type) &&
               data.IsFriendly == isFriendly &&
               levelUpData.IsFriendly == isFriendly &&
               levelUpData.AgeStage == currentAge;
    }

    public void UpdateDataPrefab<TData, TlevelUpData, TType>(
    TData data,
    TlevelUpData levelUpData)
    where TData : IUpgradable<TType>
    where TlevelUpData : IUpgradable<TType>
    {
        data.SetPrefab(levelUpData.Prefab);
    }


    public void UpgradeCoreStats(UnitData unit, UnitLevelUpData levelUpData)
    {
        unit._range += levelUpData._range;
        unit._speed += levelUpData._speed;
        unit._health += levelUpData._health;
        unit._strength += levelUpData._strength;
        unit._initialAttackDelay = Mathf.Max(0.1f, unit._initialAttackDelay - levelUpData._initialAttackDelay);

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

    private void UpdateUnitUISprite(UnitData unitData, UnitLevelUpData levelUpData)
    {
        _unitDeployButtons = UIObjectFinder.GetAllUnitDeployButtons();
        foreach (var button in _unitDeployButtons)
        {
            if (button.UnitType == unitData.Type)
            {
                var image = button.GetComponent<Image>();
                if (image != null)
                    image.sprite = levelUpData._unitSprite;

                UpgradeStateManager.Instance.SetUnitSprite(unitData.Type, image.sprite);

                break;
            }
        }
    }

    //TODO: make the UpdateSprite generic 
    private void UpdateSpecialAttackSprite(SpecialAttackData specialAttackData, SpecialAttackLevelUpData levelUpData)
    {
        _specialAttackButton = UIObjectFinder.GetSpecialAttackButton();

        if (_specialAttackButton.Type == specialAttackData.Type)
        {
            var image = _specialAttackButton.GetComponent<Image>();
            if (image != null)
                image.sprite = levelUpData.SpecialAttackSprite;

            UpgradeStateManager.Instance.SetSpecialAttackSprite(specialAttackData.Type, image.sprite);
        }

    }


    //TODO: connect the correct enum to the turret button 



    //private void UpdateTurretSprite(TurretData turretData, TurretLevelUpData levelUpData)
    //{
    //    _turretButton = UIObjectFinder.GetTurretButton();

    //    if (_turretButton.Type == turretData.Type)
    //    {
    //        var image = _turretButton.GetComponent<Image>();
    //        if (image != null)
    //            image.sprite = levelUpData.TurretSprite;

    //        UpgradeStateManager.Instance.SetSTurretSprite(turretData.Type, image.sprite);
    //    }

    //}





    #endregion



}