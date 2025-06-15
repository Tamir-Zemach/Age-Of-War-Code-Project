using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DeployManager : MonoBehaviour
{
    public static event Action OnQueueChanged;
    public static DeployManager Instance;

    [SerializeField] private Transform _unitSpawnPoint;

    public Queue<Unit> _unitQueue = new Queue<Unit>();
    private bool isDeploying = false;

    [Tooltip("The spawn area where the friendly unit will not spawn if another one is already present.")]
    [SerializeField, TagSelector] private string _spawnArea;

    [Tooltip("The player base Tag:")]
    [SerializeField, TagSelector] private string _baseTag;
    private SpawnArea SpawnArea;

    private float timer;
    private Unit nextCharacter;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError("Multiple DeployManager instances detected!");
            Destroy(gameObject);
        }
        SpawnArea = GameObject.FindGameObjectWithTag(_baseTag).GetComponentInChildren<SpawnArea>();

    }

    private void Update()
    {
        if (nextCharacter != null)
        {
            DeployWithDelay(nextCharacter._characterPrefab, nextCharacter._deployDelayTime);
        }
    }

    private void DeployWithDelay(GameObject characterPrefab, float deployDelayTime)
    {
        timer += Time.deltaTime;
        if (timer >= nextCharacter._deployDelayTime)
        {
            if (!SpawnArea._hasUnitInside)
            {
                Instantiate(characterPrefab, _unitSpawnPoint.position, _unitSpawnPoint.rotation);
                timer = 0;
                isDeploying = false;
                DeployNextCharacter();
            }
        }
    }

    public void UnitButtonPressed(Unit unit)
    {
        if (PlayerCurrency.Instance.HasEnoughMoney(unit._cost))
        {
            PlayerCurrency.Instance.SubtractMoney(unit._cost);

            // Add the character to the queue
            _unitQueue.Enqueue(unit);
            OnQueueChanged?.Invoke();

            // If no deployment is currently happening, start the queue process
            if (!isDeploying)
            {
                DeployNextCharacter();
            }
        }
    }

    private void DeployNextCharacter()
    {
        if (_unitQueue.Count > 0)
        {
            //remove button from queue
            nextCharacter = _unitQueue.Dequeue();
            OnQueueChanged?.Invoke();
            //deploy logic
            isDeploying = true;

        }
        else
        {
            nextCharacter = null;
            isDeploying = false;
        }
    }


}
