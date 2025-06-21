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
    public static Dictionary<UnitType, Unit> ModifiedUnitData { get; private set; }
    public static GameManager Instance;

    [SerializeField] private int _startingMoney;
    [SerializeField] private int _startingHealth;
    [SerializeField] private int _level1EnemyBaseHealth;
    public int Level1EnemyBaseHealth { get; private set; }

    private void Awake()
    {
        Instance = this;
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

    private void StartGame()
    {
        PlayerCurrency.Instance.AddMoney(_startingMoney);
        PlayerHealth.Instance.SetMaxHealth(_startingHealth);
        PlayerHealth.Instance.FullHealth();
        Level1EnemyBaseHealth = _level1EnemyBaseHealth;
    }

    private void CreateScriptableObjInstance()
    {
        ModifiedUnitData = new Dictionary<UnitType, Unit>();
        InstantiateFriendlyUnits();
        InstantiateFriendlyTurretData();
    }
    private void InstantiateFriendlyUnits()
    {
        Unit[] allUnits = Resources.LoadAll<Unit>("");
        foreach (var unit in allUnits)
        {
            if (unit.isFriendly)
                ModifiedUnitData[unit.unitType] = Instantiate(unit);
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

    public Unit GetInstantiatedUnit(UnitType type)
    {
        if (ModifiedUnitData != null && ModifiedUnitData.TryGetValue(type, out var unit))
        {
            return unit;
        }
        else
        {
            Debug.LogWarning($"Unit of type {type} not found in ModifiedUnitData.");
            return null;
        }
    }
    public List<Unit> GetAllInstantiatedUnits()
    {
        var allUnits = new List<Unit>();

        if (ModifiedUnitData != null)
        {
            foreach (var kvp in ModifiedUnitData)
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









