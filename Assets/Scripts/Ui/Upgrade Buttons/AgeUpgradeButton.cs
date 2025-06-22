using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.units;
using UnityEngine;
using UnityEngine.UI;

public class AgeUpgradeButton : MonoBehaviour
{
    private List<UnitData> _unitData;
    private List<LevelUpData> _levelUpData;
    [SerializeField] private UnitDeployButton[] _unitDeployButtons;
    [SerializeField] private Image[] _deployButtonSprites;
    [SerializeField] private int _ageUpgradeCost;

    private void Start()
    {
        _unitData = GameManager.Instance.GetAllInstantiatedFriendlyUnits();
        _levelUpData = Resources.LoadAll<LevelUpData>("").ToList();
        GetAllunitDeployButtons();

    }
    private void GetAllunitDeployButtons()
    {
        _unitDeployButtons = FindObjectsByType<UnitDeployButton>(FindObjectsSortMode.None);
        _deployButtonSprites = new Image[_unitDeployButtons.Length];

        for (int i = 0; i < _unitDeployButtons.Length; i++)
        {
            _deployButtonSprites[i] = _unitDeployButtons[i].GetComponent<Image>();
        }
    }


    public void UpdateAge()
    {
        if (PlayerCurrency.Instance.HasEnoughMoney(_ageUpgradeCost))
        {
            PlayerCurrency.Instance.SubtractMoney(_ageUpgradeCost);
            GameManager.Instance.AdvanceAge();
            UpgradeLogic();
        }
    }
    private void UpgradeLogic()
    {
        if (_unitData == null || _levelUpData == null)

        {
            Debug.LogWarning($"Upgrade failed: UnitData is {_unitData?.Count} items," +
                $" LevelUpData is {_levelUpData?.Count} items");
            return;
        }
        for (int i = 0; i < _unitData.Count; i++)
        {
            foreach (LevelUpData levelUpData in _levelUpData)
            {
                if (IsMatchingUpgrade(_unitData[i], levelUpData, GameManager.Instance.CurrentAge))
                {
                    UpgradeStats(_unitData[i], levelUpData);
                    ChangePrefabsAndUi(_unitData[i], levelUpData);
                    break;
                }
            }
        }
    }
    private bool IsMatchingUpgrade(UnitData unit, LevelUpData levelUpData, int currentAge)
    {
        return unit.unitType == levelUpData.unitType &&
               unit.isFriendly == levelUpData.isFriendly &&
               (int)levelUpData.ageStage == currentAge;
    }

    private void UpgradeStats(UnitData unit, LevelUpData levelUpData)
    {

        unit._range += levelUpData._range;
        unit._speed += levelUpData._speed;
        unit._health += levelUpData._health;
        unit._strength += levelUpData._strength;
        unit._initialAttackDelay = Mathf.Max(0.1f, unit._initialAttackDelay - levelUpData._initialAttackDelay);
        unit._moneyWhenKilled += levelUpData._moneyWhenKilled;
    }
    private void ChangePrefabsAndUi(UnitData unit, LevelUpData levelUpData)
    {
        unit._unitPrefab = levelUpData._unitPrefab;

        for (int i = 0; i < _unitDeployButtons.Length; i++)
        {
            if (_unitDeployButtons[i].UnitType == unit.unitType)
            {
                _deployButtonSprites[i].sprite = levelUpData._spriteForUi;
                break;
            }
        }
    }

}
