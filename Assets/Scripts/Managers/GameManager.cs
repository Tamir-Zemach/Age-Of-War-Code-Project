using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Enems;
using Assets.Scripts.Managers;
using Assets.Scripts.turrets;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TurretData _friendlyturretData;
    public static Dictionary<UnitType, UnitData> ModifiedFriendlyUnitData { get; private set; }
    public static Dictionary<UnitType, UnitData> ModifiedEnemyUnitData { get; private set; }

    public static GameManager Instance;

    [SerializeField] private int _startingMoney;
    [SerializeField] private int _startingHealth;
    [SerializeField] private int _level1EnemyBaseHealth;
    public int Level1EnemyBaseHealth { get; private set; }
    public int CurrentAge { get; private set; } = 1;

    public void AdvanceAge()
    {
        CurrentAge++;
    }

    private void Awake()
    {
        InstantiateOneGameManager();
        StartGame();
        CreateScriptableObjInstance();
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
    private void InstantiateOneGameManager()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // ← This keeps the GameManager alive between scenes
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
}









