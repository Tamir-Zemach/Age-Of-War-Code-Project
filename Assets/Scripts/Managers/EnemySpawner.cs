using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("List of enemy prefabs available for spawning.")]
    [SerializeField] private GameObject[] _enemyPrefabsList;

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
        if (_enemyPrefabsList.Length == 0) return;

        int randomIndex = Random.Range(0, _enemyPrefabsList.Length);
        Instantiate(_enemyPrefabsList[randomIndex], _enemySpawnPoint.position, _enemySpawnPoint.rotation);
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

}