
using System;
using System.Collections.Generic;
using UnityEngine;

public class DeployManager : PersistentMonoBehaviour<DeployManager> 
{
    public static event Action OnQueueChanged;

    public readonly Queue<UnitData> _unitQueue = new Queue<UnitData>();
    private bool isDeploying = false;

    [Tooltip("The position a where Friendly units will be spawned in the world.")]
    [SerializeField] private Transform _unitSpawnPoint;

    [Tooltip("The spawn area where the friendly unit will not spawn if another one is already present.")]
    [SerializeField, TagSelector] private string _spawnArea;

    [Tooltip("The player base Tag:")]
    [SerializeField, TagSelector] private string _baseTag;

    private SpawnArea SpawnArea;
    private UnitData nextCharacter;
    private GameObject unitReference;

    private float timer;

    protected override void Awake()
    {
       base.Awake();
        SpawnArea = GameObject.FindGameObjectWithTag(_spawnArea).GetComponent<SpawnArea>();

    }

    private void Update()
    {
        if (nextCharacter != null)
            HandleDelayedDeployment();
    }



    /// <summary>
    /// Adds the specified unit to the deployment queue.
    /// If no deployment is currently in progress, starts deploying immediately.
    /// </summary>
    /// <param name="unit">The UnitData to queue for deployment.</param>
    public void AddUnitToDeploymentQueue(UnitData unit)
    {
        _unitQueue.Enqueue(unit);

        //Event invoke for Ui purpses 
        OnQueueChanged?.Invoke();

        // If no deployment is currently happening, start the queue process
        if (!isDeploying)
        {
            ProcessNextUnitInQueue();
        }
    }

    /// <summary>
    /// Retrieves the next unit from the queue and marks deployment as in progress.
    /// If the queue is empty, resets deployment state.
    /// </summary>

    private void ProcessNextUnitInQueue()
    {
        if (_unitQueue.Count > 0)
        {
            //remove unit from queue
            nextCharacter = _unitQueue.Dequeue();

            //Event invoke for Ui purpses 
            OnQueueChanged?.Invoke();

            isDeploying = true;
        }
        else
        {
            nextCharacter = null;
            isDeploying = false;
        }
    }



    /// <summary>
    /// Handles unit instantiation after a delay.
    /// Waits for sufficient time and an available spawn area before deploying.
    /// Resets timer and triggers the next deployment if available.
    /// </summary>

    private void HandleDelayedDeployment()
    {
        timer += Time.deltaTime;
        if (timer >= nextCharacter._deployDelayTime && !SpawnArea._hasUnitInside)
        {
            unitReference = Instantiate(nextCharacter._unitPrefab, _unitSpawnPoint.position, _unitSpawnPoint.rotation);

            if (unitReference.TryGetComponent(out UnitBaseBehaviour behaviour))
            {
                behaviour.Initialize(nextCharacter);
            }

            timer = 0;
            isDeploying = false;
            ProcessNextUnitInQueue();
        }
    }
    

}




