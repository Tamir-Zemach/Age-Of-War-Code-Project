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
    private Dictionary<TurretType, Sprite> _turretSprites = new();

    // -- COST UPGRADES --
    private Dictionary<UnitType, int> _unitStatUpgradeCost = new();
    private int _playerStatUpgradeCost;

    // -- PREFAB UPGRADES --
    private Dictionary<UnitType, GameObject> _unitPrefabs = new();
    private Dictionary<SpecialAttackType, GameObject> _specialAttackPrefabs = new();
    private Dictionary<TurretType, GameObject> _turretPrefabs = new();

    // -- TYPES INITIELIZE --
    public SpecialAttackType CurrentSpecialAttack { get; private set; } = SpecialAttackType.MeteorRain;
    public TurretType CurrentTrret { get; private set; } = TurretType.StoneAgeTurret;

    #endregion

    #region SETTERS

    // -- UI --
    public void SetUnitSprite(UnitType type, Sprite sprite) => _unitUISprites[type] = sprite;
    public void SetSpecialAttackSprite(SpecialAttackType type, Sprite sprite) => _specialAttackSprites[type] = sprite;
    public void SetSTurretSprite(TurretType type, Sprite sprite) => _turretSprites[type] = sprite;

    // -- TYPE --
    public void UpgradeSpecialAttack(SpecialAttackType type) => CurrentSpecialAttack = type;
    public void UpgradeTurret(TurretType type) => CurrentTrret = type;

    // -- COST --
    public void SetStatUpgradeCost(UnitType type, int cost) => _unitStatUpgradeCost[type] = cost;
    public void SetPlayerStatUpgradeCost(int cost) => _playerStatUpgradeCost = cost;

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
    public int GetStatUpgradeCost(UnitType type, int defaultCost) =>
        _unitStatUpgradeCost.TryGetValue(type, out var cost) ? cost : defaultCost;

    /// <summary> Gets player stat upgrade cost or returns the default if not set. </summary>
    public int GetPlayerStatCost(int defaultCost) =>
        _playerStatUpgradeCost > 0 ? _playerStatUpgradeCost : defaultCost;

    public GameObject GetUnitPrefab(UnitType type) => _unitPrefabs.TryGetValue(type, out var prefab) ? prefab : null;
    public GameObject GetTurretPrefab(TurretType type) => _turretPrefabs.TryGetValue(type, out var prefab) ? prefab : null;
    public GameObject GetSpecialAttackPrefab(SpecialAttackType type) => _specialAttackPrefabs.TryGetValue(type, out var prefab) ? prefab : null;

    #endregion
}