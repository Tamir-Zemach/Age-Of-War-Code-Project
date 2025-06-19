using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Enems;
using Assets.Scripts.Managers;
using Assets.Scripts.turrets;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TurretData _turretData;
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

        Unit[] friendlyUnits = Resources.LoadAll<Unit>("Friendly Units");
        foreach (var unit in friendlyUnits)
        {
            ModifiedUnitData[unit.unitType] = Instantiate(unit);
        }

        _turretData = Instantiate(Resources.Load<TurretData>("FriendlyTurret"));
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
        if (_turretData != null)
        {
            return _turretData;
        }
        else
        {
            Debug.LogWarning($"TurretData not found");
            return null;
        }
    }
}









