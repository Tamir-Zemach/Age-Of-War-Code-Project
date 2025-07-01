using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enems;


/// <summary>
/// Manages upgrade-related data and behavior for units, turrets, and special attacks.
/// Handles UI sprites, prefabs, upgrade costs, and the player's current special attack selection.
/// </summary>

public class UpgradeStateManager : PersistentMonoBehaviour<UpgradeStateManager>
{
    #region LOCAL PARAMETERS

    // -- UI SPRITE STATE --
    private Dictionary<UnitType, Sprite> _unitUISprites = new();
    private Dictionary<SpecialAttackType, Sprite> _specialAttackSprites = new();
    private Sprite _friendlyTurretUISprite;

    // -- COST UPGRADES --
    private Dictionary<UnitType, int> _unitStatUpgradeCost = new();
    private int _playerStatUpgradeCost;

    // -- PREFAB UPGRADES --
    private Dictionary<UnitType, GameObject> _unitPrefabs = new();
    private GameObject _friendlyTurretPrefab;

    // -- SPECIAL ATTACK TYPE --
    public SpecialAttackType CurrentSpecialAttack { get; private set; } = SpecialAttackType.MeteorRain;

    #endregion

    #region SETTERS

    public void SetUnitSprite(UnitType type, Sprite sprite) => _unitUISprites[type] = sprite;
    public void SetSpecialAttackSprite(SpecialAttackType type, Sprite sprite) => _specialAttackSprites[type] = sprite;
    public void SetTurretSprite(Sprite sprite) => _friendlyTurretUISprite = sprite;

    public void SetStatUpgradeCost(UnitType type, int cost) => _unitStatUpgradeCost[type] = cost;
    public void SetPlayerStatUpgradeCost(int cost) => _playerStatUpgradeCost = cost;

    public void SetUnitPrefab(UnitType type, GameObject prefab) => _unitPrefabs[type] = prefab;
    public void SetTurretPrefab(GameObject prefab) => _friendlyTurretPrefab = prefab;

    public void UpgradeSpecialAttack(SpecialAttackType type) => CurrentSpecialAttack = type;

    #endregion


    #region GETTERS
    
    public Sprite GetUnitSprite(UnitType type) => _unitUISprites.TryGetValue(type, out var sprite) ? sprite : null;

    public Sprite GetSpecialAttackSprite(SpecialAttackType type) => _specialAttackSprites.TryGetValue(type, out var sprite) ? sprite : null;

    public Sprite GetTurretSprite() => _friendlyTurretUISprite;

    /// <summary> Gets upgrade cost for a unit type or returns the default if missing. </summary>
    public int GetStatUpgradeCost(UnitType type, int defaultCost) =>
        _unitStatUpgradeCost.TryGetValue(type, out var cost) ? cost : defaultCost;

    /// <summary> Gets player stat upgrade cost or returns the default if not set. </summary>
    public int GetPlayerStatCost(int defaultCost) =>
        _playerStatUpgradeCost > 0 ? _playerStatUpgradeCost : defaultCost;

    public GameObject GetUnitPrefab(UnitType type) => _unitPrefabs.TryGetValue(type, out var prefab) ? prefab : null;
    public GameObject GetTurretPrefab() => _friendlyTurretPrefab;

    #endregion
}