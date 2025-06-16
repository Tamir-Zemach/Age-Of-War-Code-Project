
using System.Collections.Generic;
using System.Xml.Serialization;
using Assets.Scripts;
using Assets.Scripts.Enems;
using Assets.Scripts.Managers;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("Scriptable Objects")]
    [SerializeField] private Unit[] _baseUnitAsset;
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
        PlayerHealth.Instance.DisplyHealthInConsole();
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
        PlayerHealth.Instance.AddHealth(_startingHealth);
        Level1EnemyBaseHealth = _level1EnemyBaseHealth;
    }
    private void CreateScriptableObjInstance()
    {
        ModifiedUnitData = new Dictionary<UnitType, Unit>();

        foreach (var unit in _baseUnitAsset)
        {
            ModifiedUnitData[unit.unitType] = Instantiate(unit);
        }
    }
}









