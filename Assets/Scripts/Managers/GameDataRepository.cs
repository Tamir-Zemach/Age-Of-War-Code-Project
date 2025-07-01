using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.Data;
using Assets.Scripts.Enems;
using Assets.Scripts.turrets;

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
    }

    private void LoadUnitData()
    {
        _friendlyUnits = new Dictionary<UnitType, UnitData>();
        _enemyUnits = new Dictionary<UnitType, UnitData>();

        foreach (var unit in Resources.LoadAll<UnitData>(""))
        {
            var clone = Instantiate(unit);
            if (unit.isFriendly)
                _friendlyUnits[unit.unitType] = clone;
            else
                _enemyUnits[unit.unitType] = clone;
        }
    }

    private void LoadTurretData()
    {
        var turret = Resources.LoadAll<TurretData>("").FirstOrDefault(t => t.isFriendly);
        _friendlyTurret = turret != null ? Instantiate(turret) : null;

        if (_friendlyTurret == null)
            Debug.LogWarning("No friendly turret data found.");
    }

    private void LoadSpecialAttackData()
    {
        var selected = UpgradeStateManager.Instance.CurrentSpecialAttack;

        var match = Resources.LoadAll<SpecialAttackData>("")
            .FirstOrDefault(s => s.specialAttackType == selected);

        _friendlySpecialAttack = match != null ? Instantiate(match) : null;

        if (_friendlySpecialAttack == null)
            Debug.LogWarning($"No SpecialAttackData found for type {selected}");
    }

    public UnitData GetFriendlyUnit(UnitType type) =>
        _friendlyUnits.TryGetValue(type, out var data) ? data : null;

    public UnitData GetEnemyUnit(UnitType type) =>
        _enemyUnits.TryGetValue(type, out var data) ? data : null;

    public List<UnitData> GetAllFriendlyUnits() => _friendlyUnits?.Values.ToList();
    public List<UnitData> GetAllEnemyUnits() => _enemyUnits?.Values.ToList();

    public TurretData GetFriendlyTurret() => _friendlyTurret;
    public SpecialAttackData GetSpecialAttack() => _friendlySpecialAttack;
}