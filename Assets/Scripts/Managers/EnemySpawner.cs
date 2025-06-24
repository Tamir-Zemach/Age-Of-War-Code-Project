using System.Collections.Generic;
using Assets.Scripts.Enems;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

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
        if (GameManager.ModifiedEnemyUnitData.Count == 0) return;

        var enemyUnits = new List<UnitData>(GameManager.ModifiedEnemyUnitData.Values);
        UnitData randomEnemyData = enemyUnits[Random.Range(0, enemyUnits.Count)];

        GameObject enemyReference = Instantiate(
            randomEnemyData._unitPrefab,
            _enemySpawnPoint.position,
            _enemySpawnPoint.rotation
        );

        UnitBaseBehaviour behaviour = enemyReference.GetComponent<UnitBaseBehaviour>();

        if (behaviour != null)
        {
            behaviour.Initialize(randomEnemyData);
        }
        else
        {
            Debug.LogWarning("UnitBaseBehaviour not found on spawned enemy prefab.");
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

    public List<UnitData> GetAllInstantiatedUnits()
    {
        var allUnits = new List<UnitData>();

        if (GameManager.ModifiedEnemyUnitData != null)
        {
            foreach (var kvp in GameManager.ModifiedEnemyUnitData)
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