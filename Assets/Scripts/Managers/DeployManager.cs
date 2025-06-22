using Assets.Scripts;
using Assets.Scripts.Enems;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DeployManager : MonoBehaviour
{
    public static event Action OnQueueChanged;
    public static DeployManager Instance;

    [SerializeField] private Transform _unitSpawnPoint;

    public Queue<UnitData> _unitQueue = new Queue<UnitData>();
    private bool isDeploying = false;

    [Tooltip("The spawn area where the friendly unit will not spawn if another one is already present.")]
    [SerializeField, TagSelector] private string _spawnArea;

    [Tooltip("The player base Tag:")]
    [SerializeField, TagSelector] private string _baseTag;
    private SpawnArea SpawnArea;

    private float timer;
    private UnitData nextCharacter;
    GameObject unitReference;
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
          DeployWithDelay();
    }

    public void DeployAttackerUnit()
    {
        UnitData unit = GameManager.Instance.GetInstantiatedUnit(UnitType.Attacker);
        UnitButtonPressed(unit);
    }
    public void DeployRangerUnit()
    {
        UnitData unit = GameManager.Instance.GetInstantiatedUnit(UnitType.Ranger);
        UnitButtonPressed(unit);
    }
    public void DeployTankUnit()
    {
        UnitData unit = GameManager.Instance.GetInstantiatedUnit(UnitType.Tank);
        UnitButtonPressed(unit);
    }
    private void UnitButtonPressed(UnitData unit)
    {
        if (PlayerCurrency.Instance.HasEnoughMoney(unit._cost))
        {
            PlayerCurrency.Instance.SubtractMoney(unit._cost);

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
    private void DeployWithDelay()
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
            DeployNextCharacter();
        }
    }

}
