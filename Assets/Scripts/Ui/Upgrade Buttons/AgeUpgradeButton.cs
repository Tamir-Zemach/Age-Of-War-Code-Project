using Assets.Scripts;
using Assets.Scripts.Enems;
using Assets.Scripts.units;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AgeUpgradeButton : MonoBehaviour
{
    private List<TurretAndSpecialAttackLevelUpData> _turretAndSpecialAttackData;

    private List<UnitData> _friendlyUnitData;
    private List<UnitLevelUpData> _unitLevelUpData;
    private UnitDeployButton[] _unitDeployButtons;
    private Image[] _deployButtonImages;
    private SpecialAttackButton _specialAttackButton;
    private Image _specialAttckButtonImage;
    [SerializeField] private int _ageUpgradeCost;

    private void Start()
    {
        _friendlyUnitData = GameManager.Instance.GetAllInstantiatedFriendlyUnits();
        _unitLevelUpData = Resources.LoadAll<UnitLevelUpData>("").ToList(); 
        _turretAndSpecialAttackData = Resources.LoadAll<TurretAndSpecialAttackLevelUpData>("").ToList();
        GetAllunitDeployButtons();
        GetSpecialAttackButton();
    }

    /// <summary>
    /// Retrieves all unit deployment buttons (UnitDeployButton) in the scene
    /// and caches their associated Image components for later UI updates.
    /// </summary>
    private void GetAllunitDeployButtons()
    {
        _unitDeployButtons = FindObjectsByType<UnitDeployButton>(FindObjectsSortMode.None);
        _deployButtonImages = new Image[_unitDeployButtons.Length];

        for (int i = 0; i < _unitDeployButtons.Length; i++)
        {
            _deployButtonImages[i] = _unitDeployButtons[i].GetComponent<Image>();
        }
    }
    private void GetSpecialAttackButton()
    {
        _specialAttackButton = FindFirstObjectByType<SpecialAttackButton>();

        if (_specialAttackButton != null)
        {
            _specialAttckButtonImage = _specialAttackButton.GetComponent<Image>();
        }
        else
        {
            Debug.LogWarning("SpecialAttackButton not found in the scene.");
        }
    }

    public void UpdateAge()
    {
        if (PlayerCurrency.Instance.HasEnoughMoney(_ageUpgradeCost))
        {
            PlayerCurrency.Instance.SubtractMoney(_ageUpgradeCost);
            AgeUpgrade.Instance.AdvanceAge(true);
            ApplyUpgradesToUnits();
            ApplyTurretAndSpecialAttackUpgrade();
        }
    }

    /// <summary>
    /// Iterates through all friendly units and applies stat and UI upgrades
    /// based on matching LevelUpData and the player's current age.
    /// </summary>
    private void ApplyUpgradesToUnits()
    {
        if (NullCheck())
        {
            return;
        }

        for (int i = 0; i < _friendlyUnitData.Count; i++)
        {
            foreach (UnitLevelUpData levelUpData in _unitLevelUpData)
            {
                if (IsUpgradeRelevant(_friendlyUnitData[i], levelUpData, AgeUpgrade.Instance.CurrentPlayerAge))
                {
                    AgeUpgrade.Instance.UpgradeCoreStats(_friendlyUnitData[i], levelUpData);
                    UpdateUnitReward(_friendlyUnitData[i], levelUpData);
                    UpdateUnitUISprite(_friendlyUnitData[i], levelUpData);

                    // ✅ Remember these changes for future scenes
                    UpgradeStateManager.Instance.SetUnitSprite(_friendlyUnitData[i].unitType, levelUpData._unitSprite);
                    UpgradeStateManager.Instance.SetUnitPrefab(_friendlyUnitData[i].unitType, levelUpData._unitPrefab);
                    break;
                }
            }
        }
    }

    // 📦 New method just once per age
    private void ApplyTurretAndSpecialAttackUpgrade()
    {
        var currentAge = AgeUpgrade.Instance.CurrentPlayerAge;
        foreach (var data in _turretAndSpecialAttackData)
        {
            if ((int)data.ageStage == currentAge)
            {
                // Set special attack sprite and type
                UpgradeStateManager.Instance.UpgradeSpecialAttack(data.specialAttackType);
                UpgradeStateManager.Instance.SetSpecialAttackSprite(data.specialAttackType, data._SpecialAttackSprite);

                if (_specialAttckButtonImage != null && data._SpecialAttackSprite != null)
                {
                    _specialAttckButtonImage.sprite = data._SpecialAttackSprite;
                }

                // Set turret button sprite
                if (data._TurretSprite != null)
                {
                    UpgradeStateManager.Instance.SetTurretSprite(data._TurretSprite);
                    // You can update your turret button UI here too if needed
                }

                break;
            }
        }
    }




    /// <summary>
    /// Determines if a unit qualifies for an upgrade based on matching unit type,
    /// allegiance (friendly/enemy), and the current age stage.
    /// </summary>
    /// <param name="unit">The runtime unit data to evaluate.</param>
    /// <param name="levelUpData">The level-up data entry to check against.</param>
    /// <param name="currentAge">The player's current age stage.</param>
    /// <returns>True if this upgrade applies to the given unit; otherwise, false.</returns>
    private bool IsUpgradeRelevant(UnitData unit, UnitLevelUpData levelUpData, int currentAge)
    {
        return unit.unitType == levelUpData.unitType &&
               unit.isFriendly == levelUpData.isFriendly &&
               (int)levelUpData.ageStage == currentAge;
    }



    /// <summary>
    /// Increases the reward value granted when the specified unit is defeated,
    /// using the additional amount defined in the corresponding level-up data.
    /// </summary>
    /// <param name="unit">The unit whose reward value should be increased.</param>
    /// <param name="levelUpData">The data containing the reward increment.</param>
    private void UpdateUnitReward(UnitData unit, UnitLevelUpData levelUpData)
    {
        unit._moneyWhenKilled += levelUpData._moneyWhenKilled;
    }



    /// <summary>
    /// Updates the deployment UI button sprite to reflect the unit's upgraded visual,
    /// based on the level-up data associated with that unit type.
    /// </summary>
    /// <param name="unit">The upgraded unit whose UI needs to be refreshed.</param>
    /// <param name="levelUpData">The data containing the new sprite for the button.</param>
    private void UpdateUnitUISprite(UnitData unit, UnitLevelUpData levelUpData)
    {
        for (int i = 0; i < _unitDeployButtons.Length; i++)
        {
            if (_unitDeployButtons[i].UnitType == unit.unitType)
            {
                _deployButtonImages[i].sprite = levelUpData._unitSprite;
                break;
            }
        }
    }


    private bool NullCheck()
    {
        if (_friendlyUnitData == null || _unitLevelUpData == null)
        {
            Debug.LogWarning($"Upgrade failed: UnitData is {_friendlyUnitData?.Count} items," +
                  $" LevelUpData is {_unitLevelUpData?.Count} items");
            return true;
        }
        else
        {
            return false;
        }
    }


}
