using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enems;


/// <summary>
/// Manages upgrade-related data and behavior for units, turrets, and special attacks.
/// Handles UI sprites, prefabs, upgrade costs, and the player's current special attack selection.
///</summary>
/// <remarks> 
/// To Debug: Tools => Upgrade State Manager Debugger in runtime. 
/// </remarks>

public class UpgradeStateManager : PersistentMonoBehaviour<UpgradeStateManager>
{
    #region LOCAL PARAMETERS

    // -- UI SPRITE STATE --
    private Dictionary<UnitType, Sprite> _unitUISprites = new();
    private Dictionary<SpecialAttackType, Sprite> _specialAttackSprites = new();
    private Dictionary<TurretType, Sprite> _turretSprites = new();

    // -- COST UPGRADES --
    private Dictionary<(UnitType, StatType), int> _unitStatUpgradeCost = new();
    private Dictionary<UpgradeType, int> _playerStatUpgradeCost = new();

    // -- PREFAB UPGRADES --
    private Dictionary<UnitType, GameObject> _unitPrefabs = new();
    private Dictionary<SpecialAttackType, GameObject> _specialAttackPrefabs = new();
    private Dictionary<TurretType, GameObject> _turretPrefabs = new();

    // -- TYPES INITIELIZE --
    public SpecialAttackType CurrentSpecialAttack { get; private set; } = SpecialAttackType.MeteorRain;
    public TurretType CurrentTurret { get; private set; } = TurretType.StoneAgeTurret;

    #endregion

    #region SETTERS

    // -- UI --
    public void SetUnitSprite(UnitType type, Sprite sprite) => _unitUISprites[type] = sprite;
    public void SetSpecialAttackSprite(SpecialAttackType type, Sprite sprite) => _specialAttackSprites[type] = sprite;
    public void SetSTurretSprite(TurretType type, Sprite sprite) => _turretSprites[type] = sprite;

    // -- TYPE --
    public void UpgradeSpecialAttack(SpecialAttackType type) => CurrentSpecialAttack = type;
    public void UpgradeTurret(TurretType type) => CurrentTurret = type;

    // -- COST --
    public void SetStatUpgradeCost(UnitType unitType,StatType statType, int cost) => _unitStatUpgradeCost[(unitType, statType)] = cost;
    public void SetPlayerStatUpgradeCost(UpgradeType type ,int cost) => _playerStatUpgradeCost[type] = cost;

    // -- PREFABS --
    public void SetUnitPrefab(UnitType type, GameObject prefab) => _unitPrefabs[type] = prefab;
    public void SetSpecialAttackPrefab(SpecialAttackType type, GameObject prefab) => _specialAttackPrefabs[type] = prefab;
    public void SetTurretPrefab(TurretType type, GameObject prefab) => _turretPrefabs[type] = prefab;

    #endregion


    #region GETTERS

    public Sprite GetUnitSprite(UnitType type) => _unitUISprites.TryGetValue(type, out var sprite) ? sprite : null;

    public Sprite GetSpecialAttackSprite(SpecialAttackType type) => _specialAttackSprites.TryGetValue(type, out var sprite) ? sprite : null;

    public Sprite GetTurretSprite(TurretType type) => _turretSprites.TryGetValue(type, out var sprite) ? sprite : null;

    /// <summary> Gets upgrade cost for a unit type or returns the default if missing. </summary>
    public int GetStatUpgradeCost(UnitType unitType,StatType statType, int defaultCost) 
    {
        return _unitStatUpgradeCost.TryGetValue((unitType, statType), out var cost) ? cost : defaultCost;
    }
       

    /// <summary> Gets player stat upgrade cost or returns the default if not set. </summary>
    public int GetPlayerStatCost(UpgradeType type, int defaultCost)
    {
        return _playerStatUpgradeCost.TryGetValue(type, out var cost) ? cost : defaultCost;
    }
       

    public GameObject GetUnitPrefab(UnitType type) => _unitPrefabs.TryGetValue(type, out var prefab) ? prefab : null;
    public GameObject GetTurretPrefab() => _turretPrefabs.TryGetValue(CurrentTurret, out var prefab) ? prefab : null;
    public GameObject GetSpecialAttackPrefab() => _specialAttackPrefabs.TryGetValue(CurrentSpecialAttack, out var prefab) ? prefab : null;

    
    // -- DEBUG GETTERS -- 
    public Dictionary<(UnitType, StatType), int> GetAllUnitStatUpgradeCosts() => _unitStatUpgradeCost;
    public Dictionary<UpgradeType, int> GetAllPlayerUpgradeCosts() => _playerStatUpgradeCost;
    public Dictionary<UnitType, GameObject> GetAllUnitPrefabs() => _unitPrefabs;
    public Dictionary<SpecialAttackType, GameObject> GetAllSpecialAttackPrefabs() => _specialAttackPrefabs;
    public Dictionary<TurretType, GameObject> GetAllTurretPrefabs() => _turretPrefabs;

    #endregion
}