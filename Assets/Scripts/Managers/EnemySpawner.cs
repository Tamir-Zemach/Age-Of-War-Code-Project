using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabsList;
    [SerializeField, TagSelector] private string _enemyBaseTag;
    [SerializeField] private Transform _enemySpawnPoint;
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;
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
        return _timer >= _randomSpawnTimer && _enemySpawnArea != null && !_enemySpawnArea._hasUnitInside;
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
}