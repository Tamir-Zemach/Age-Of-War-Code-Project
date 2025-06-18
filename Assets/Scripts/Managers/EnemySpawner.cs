using System.Collections.Generic;
using Assets.Scripts.Enems;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    public static Dictionary<UnitType, Unit> _enemyUnitData = new Dictionary<UnitType, Unit>();

    [Tooltip("Enemy Unit ScriptableObjects")]
    [SerializeField] private Unit[] _enemyUnitAssets;

    [Tooltip("Tag used to identify the enemy base in the scene.")]
    [SerializeField, TagSelector] private string _enemyBaseTag;

    [Tooltip("The spawn point where enemies will appear.")]
    [SerializeField] private Transform _enemySpawnPoint;

    [Tooltip("Minimum time interval before an enemy can spawn (can be a decimal value).")]
    [SerializeField] private float _minSpawnTime;

    [Tooltip("Maximum time interval before an enemy can spawn (can be a decimal value).")]
    [SerializeField] private float _maxSpawnTime;

    [Tooltip("Maximum number of enemies allowed to spawn.")]
    [SerializeField] private int _maxEnemies;

    private SpawnArea _enemySpawnArea;
    private float _timer;
    private float _randomSpawnTimer;

    private void Awake()
    {
        Instance = this;
        GetEnemyBase();
        _randomSpawnTimer = Random.Range(_minSpawnTime, _maxSpawnTime);
        InitializeEnemyUnitData();
    }
    private void InitializeEnemyUnitData()
    {
        foreach (var unit in _enemyUnitAssets)
        {
            _enemyUnitData[unit.unitType] = Instantiate(unit);
        }
    }
    private void Update()
    {
        SpawnPrefabsWithRandomTime();
    }

    private void SpawnPrefabsWithRandomTime()
    {
        _timer += Time.deltaTime;
        if (CanDeploy())
        {
            SpawnRandomEnemyPrefab();
            _timer = 0;
            _randomSpawnTimer = Random.Range(_minSpawnTime, _maxSpawnTime);
        }
    }

    private void SpawnRandomEnemyPrefab()
    {
        // Don't proceed if there are no enemy unit assets assigned
        if (_enemyUnitAssets.Length == 0) return;

        int randomIndex = Random.Range(0, _enemyUnitAssets.Length);
        Unit unitData = _enemyUnitAssets[randomIndex];

        GameObject enemyReference = Instantiate(
            unitData._characterPrefab,
            _enemySpawnPoint.position,
            _enemySpawnPoint.rotation
        );

        // Retrieve the UnitBaseBehaviour component attached to the prefab
        UnitBaseBehaviour behaviour = enemyReference.GetComponent<UnitBaseBehaviour>();

        // If the component exists, inject the initialized Unit instance into its logic
        if (behaviour != null)
        {
            // Look up the pre-instantiated enemy unit data from the dictionary and assign it
            behaviour.Initialize(_enemyUnitData[unitData.unitType]);
        }
    }

    private bool CanDeploy()
    {
        return _timer >= _randomSpawnTimer && _enemySpawnArea != null && !_enemySpawnArea._hasUnitInside && EnemyCounter.EnemyCount < _maxEnemies;
    }
    private SpawnArea GetEnemyBase()
    {
        GameObject enemyBase = GameObject.FindGameObjectWithTag(_enemyBaseTag);
        if (enemyBase != null)
        {
            _enemySpawnArea = enemyBase.GetComponentInChildren<SpawnArea>();
            return _enemySpawnArea;
        }

        Debug.LogError("Enemy base not found! Check the tag assignment.");
        return null; 
    }

    public void EasyMode(float minSpawnTime, float maxSpawnTime)
    {
        _minSpawnTime = minSpawnTime;
        _maxSpawnTime = maxSpawnTime;
    }

    public List<Unit> GetAllInstantiatedUnits()
    {
        var allUnits = new List<Unit>();

        if (_enemyUnitData != null)
        {
            foreach (var kvp in _enemyUnitData)
            {
                if (kvp.Value != null)
                {
                    allUnits.Add(kvp.Value);
                }
                else
                {
                    Debug.LogWarning($"Unit of type {kvp.Key} is null in _enemyUnitData.");
                }
            }
        }
        else
        {
            Debug.LogWarning("_enemyUnitData is null.");
        }

        return allUnits;
    }


}