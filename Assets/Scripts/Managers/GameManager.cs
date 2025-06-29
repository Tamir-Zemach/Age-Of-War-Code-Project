using Assets.Scripts;
using Assets.Scripts.Data;
using Assets.Scripts.Enems;
using Assets.Scripts.Managers;
using Assets.Scripts.turrets;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;




//TODO: Arrage the whole scripts to be more readable!!!!! 




public class GameManager : PersistentMonoBehaviour<GameManager> 
{
    private TurretData _friendlyturretData;

    private SpecialAttackData _friendlySpecialAttackData;

    public static Dictionary<UnitType, UnitData> ModifiedFriendlyUnitData { get; private set; }
    public static Dictionary<UnitType, UnitData> ModifiedEnemyUnitData { get; private set; }

    
    [SerializeField] private int _startingMoney;
    [SerializeField] private int _startingHealth;
    [SerializeField] private int _level1EnemyBaseHealth;
    public int Level1EnemyBaseHealth { get; private set; }

    protected override void Awake()
    {
        base.Awake(); // ensures the singleton logic runs
        StartGame();
        CreateScriptableObjInstance();
    }
    private void Start()
    {
        InstantiateFriendlySpecialAttackData();
    }



    private void Update()
    {
        if (PlayerHealth.Instance.PlayerDied())
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
    }

    private void StartGame()
    {
        PlayerCurrency.Instance.AddMoney(_startingMoney);
        PlayerHealth.Instance.SetMaxHealth(_startingHealth);
        PlayerHealth.Instance.FullHealth();
        Level1EnemyBaseHealth = _level1EnemyBaseHealth;
    }

    private void CreateScriptableObjInstance()
    {
        ModifiedFriendlyUnitData = new Dictionary<UnitType, UnitData>();
        ModifiedEnemyUnitData = new Dictionary<UnitType, UnitData>();
        InstantiateFriendlyUnits();
        InstantiateEnemyUnits();
        InstantiateFriendlyTurretData();
    }
    private void InstantiateFriendlyUnits()
    {
        UnitData[] allUnits = Resources.LoadAll<UnitData>("");
        foreach (var unit in allUnits)
        {
            if (unit.isFriendly)
                ModifiedFriendlyUnitData[unit.unitType] = Instantiate(unit);
        }
    }
    private void InstantiateEnemyUnits()
    {
        UnitData[] allUnits = Resources.LoadAll<UnitData>("");
        foreach (var unit in allUnits)
        {
            if (!unit.isFriendly)
                ModifiedEnemyUnitData[unit.unitType] = Instantiate(unit);
        }
    }
    private void InstantiateFriendlyTurretData()
    {
        TurretData[] allTurrets = Resources.LoadAll<TurretData>("");
        var match = allTurrets.FirstOrDefault(t => t.isFriendly);

        if (match != null)
            _friendlyturretData = Instantiate(match);
        else
            Debug.LogWarning("No friendly turret data found in Resources.");
    }

    private void InstantiateFriendlySpecialAttackData()
    {
        SpecialAttackData[] allSpecials = Resources.LoadAll<SpecialAttackData>("");
        var match = allSpecials.FirstOrDefault(s => s.specialAttackType == UpgradeStateManager.Instance.CurrentSpecialAttack);

        if (match != null)
            _friendlySpecialAttackData = Instantiate(match);
        else
            Debug.LogWarning("No matching SpecialAttackData found for current attack.");
    }

    public UnitData GetInstantiatedUnit(UnitType type)
    {
        if (ModifiedFriendlyUnitData != null && ModifiedFriendlyUnitData.TryGetValue(type, out var unit))
        {
            return unit;
        }
        else
        {
            Debug.LogWarning($"Unit of type {type} not found in ModifiedUnitData.");
            return null;
        }
    }
    public List<UnitData> GetAllInstantiatedFriendlyUnits()
    {
        var allUnits = new List<UnitData>();

        if (ModifiedFriendlyUnitData != null)
        {
            foreach (var kvp in ModifiedFriendlyUnitData)
            {
                if (kvp.Value != null)
                {
                    allUnits.Add(kvp.Value);
                }
                else
                {
                    Debug.LogWarning($"Unit of type {kvp.Key} is null in ModifiedUnitData.");
                }
            }
        }
        else
        {
            Debug.LogWarning("ModifiedUnitData is null.");
        }

        return allUnits;
    }
    public List<UnitData> GetAllInstantiatedEnemyUnits()
    {
        var allUnits = new List<UnitData>();

        if (ModifiedEnemyUnitData != null)
        {
            foreach (var kvp in ModifiedEnemyUnitData)
            {
                if (kvp.Value != null)
                {
                    allUnits.Add(kvp.Value);
                }
                else
                {
                    Debug.LogWarning($"Unit of type {kvp.Key} is null in ModifiedUnitData.");
                }
            }
        }
        else
        {
            Debug.LogWarning("ModifiedUnitData is null.");
        }

        return allUnits;
    }
    public TurretData GetTurretData()
    {
        if (_friendlyturretData != null)
        {
            return _friendlyturretData;
        }
        else
        {
            Debug.LogWarning($"TurretData not found");
            return null;
        }
    }

    public SpecialAttackData GetSpecialAttackData()
    {
        if (_friendlySpecialAttackData != null)
        {
            return _friendlySpecialAttackData;
        }
        else
        {
            Debug.LogWarning("No SpecialAttackData found.");
            return null;
        }
    }


}









