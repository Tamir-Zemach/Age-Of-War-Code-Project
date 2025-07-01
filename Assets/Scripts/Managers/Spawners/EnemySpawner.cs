using Assets.Scripts.Enems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemySpawner : PersistentMonoBehaviour<EnemySpawner>
{

    [Tooltip("Tag used to identify the enemy base in the scene.")]
    [SerializeField, TagSelector] private string _spawnAreaTag;

    [Tooltip("Minimum time interval before an enemy can spawn (can be a decimal value).")]
    [SerializeField] private float _minSpawnTime;

    [Tooltip("Maximum time interval before an enemy can spawn (can be a decimal value).")]
    [SerializeField] private float _maxSpawnTime;

    [Tooltip("Maximum number of enemies allowed to spawn.")]
    [SerializeField] private int _maxEnemies;

    private SpawnArea _enemySpawnArea;
    private Transform _enemySpawnPoint;
    private float _timer;
    private float _randomSpawnTimer;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Initialize();
    }


    public void Initialize()
    {
        GameObject areaGO = GameObject.FindGameObjectWithTag(_spawnAreaTag);
        if (areaGO == null)
        {
            Debug.LogWarning($"[DeployManager] No GameObject with tag '{_spawnAreaTag}' found in scene.");
            return;
        }

        _enemySpawnArea = areaGO.GetComponent<SpawnArea>();
        if (_enemySpawnArea == null)
        {
            Debug.LogWarning($"[DeployManager] GameObject tagged '{_enemySpawnArea}' is missing SpawnArea component.");
            return;
        }

        _enemySpawnPoint = _enemySpawnArea.GetComponentInParent<Transform>();
        if (_enemySpawnPoint == null)
        {
            Debug.LogWarning("[DeployManager] Failed to locate parent transform for spawn point.");
        }
    }

    protected override void Awake()
    {
        base.Awake();
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
        var enemyUnits = GameDataRepository.Instance.GetAllEnemyUnits();

        if (enemyUnits == null || enemyUnits.Count == 0) return;

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

    public void EasyMode(float minSpawnTime, float maxSpawnTime)
    {
        _minSpawnTime = minSpawnTime;
        _maxSpawnTime = maxSpawnTime;
    }


}