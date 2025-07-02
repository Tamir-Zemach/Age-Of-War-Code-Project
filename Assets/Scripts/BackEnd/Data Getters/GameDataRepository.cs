using Assets.Scripts.Data;
using Assets.Scripts.Enems;
using Assets.Scripts.turrets;
using Assets.Scripts.units;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Central repository for loading, instantiating, and caching runtime gameplay data.
/// Handles unit, turret, and special attack ScriptableObjects, making them accessible throughout the game.
/// </summary>
public class GameDataRepository : PersistentMonoBehaviour<GameDataRepository>
{
    private Dictionary<UnitType, UnitData> _friendlyUnits;
    private Dictionary<UnitType, UnitData> _enemyUnits;
    private TurretData _friendlyTurret;
    private SpecialAttackData _friendlySpecialAttack;

    private List<UnitLevelUpData> _unitLevelUpData;
    private SpecialAttackLevelUpData _specialAttackLevelUpData;
    private TurretLevelUpData _turretLevelUpData;

    protected override void Awake()
    {
        base.Awake();
        LoadAll();
    }

    public void LoadAll( )
    {
        LoadUnitData();
        LoadTurretData();
        LoadSpecialAttackData();
        LoadLevelUpData();
    }

    private void LoadUnitData()
    {
        _friendlyUnits = new Dictionary<UnitType, UnitData>();
        _enemyUnits = new Dictionary<UnitType, UnitData>();

        foreach (var unit in Resources.LoadAll<UnitData>(""))
        {
            var clone = Instantiate(unit);
            if (unit.IsFriendly)
                _friendlyUnits[unit.Type] = clone;
            else
                _enemyUnits[unit.Type] = clone;
        }
    }

    private void LoadTurretData()
    {
        var turret = Resources.LoadAll<TurretData>("").FirstOrDefault(t => t.IsFriendly);
        _friendlyTurret = turret != null ? Instantiate(turret) : null;

        if (_friendlyTurret != null && _friendlyTurret.Prefab != null)
        {
            UpgradeStateManager.Instance.SetTurretPrefab(_friendlyTurret.Type, _friendlyTurret.Prefab);
        }
        else
        {
            Debug.LogError($"[GameDataRepository] Could not register prefab for turret type {_friendlyTurret?.Type}. Is the prefab assigned?");
        }
    }

    private void LoadSpecialAttackData()
    {
        var selected = UpgradeStateManager.Instance.CurrentSpecialAttack;

        var match = Resources.LoadAll<SpecialAttackData>("")
            .FirstOrDefault(s => s.Type == selected);

        _friendlySpecialAttack = match != null ? Instantiate(match) : null;

        if (_friendlySpecialAttack == null)
            Debug.LogWarning($"No SpecialAttackData found for type {selected}");
    }

    private void LoadLevelUpData()
    {
        _unitLevelUpData = Resources.LoadAll<UnitLevelUpData>("").ToList();

        _specialAttackLevelUpData = Resources.LoadAll<SpecialAttackLevelUpData>("").FirstOrDefault();

        _turretLevelUpData = Resources.LoadAll<TurretLevelUpData>("").FirstOrDefault();
    }


    public UnitData GetFriendlyUnit(UnitType type) =>
        _friendlyUnits.TryGetValue(type, out var data) ? data : null;

    public UnitData GetEnemyUnit(UnitType type) =>
        _enemyUnits.TryGetValue(type, out var data) ? data : null;

    public List<UnitData> GetAllFriendlyUnits() => _friendlyUnits?.Values.ToList();
    public List<UnitData> GetAllEnemyUnits() => _enemyUnits?.Values.ToList();
    public List<UnitLevelUpData> GetUnitLevelUpData() => _unitLevelUpData;
    public SpecialAttackLevelUpData GetSpecialAttackLevelUpData() => _specialAttackLevelUpData;
    public TurretLevelUpData GetTurretLevelUpData() => _turretLevelUpData;

    public TurretData GetFriendlyTurret() => _friendlyTurret;
    public SpecialAttackData GetSpecialAttack() => _friendlySpecialAttack;

}