using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Enems;
using UnityEngine;
//using UnityEngine.Events;

public class TurretButton : MonoBehaviour
{
    //events for later
    //[Header("Events")]
    //public UnityEvent onTurretPlaced;
    //public UnityEvent onSlotUnlocked;
    [SerializeField] private TurretButtonType TurretbuttonType;
    [SerializeField] private GameObject _turretPrefab;
    [SerializeField] private int _cost;

    private Transform _turretSpawnPos;

    private bool _isWaitingForClick;

    [SerializeField] private List<TurretSpawnPoint> _turretSpawnPoints = new List<TurretSpawnPoint>();

    //The actual function to use onClick
    public void DeployTurret()
    {
        if (!PlayerCurrency.Instance.HasEnoughMoney(_cost) || !CanDeployTurret()) return;

        _isWaitingForClick = true;
        FlashAllAvailablePoints();
        StartCoroutine(WaitForTurretClick());
    }
    public void AddTurretSlot()
    {
        if (!PlayerCurrency.Instance.HasEnoughMoney(_cost) || !CanAddSlot()) return;

        _isWaitingForClick = true;
        ShowAllAvailablePoints();
        StartCoroutine(WaitForSlotClick());
    }
    public void SellTurret()
    {
        if (!PlayerCurrency.Instance.HasEnoughMoney(_cost) || !CanAddSlot()) return;

        _isWaitingForClick = true;
        ShowAllAvailablePoints();
        StartCoroutine(WaitForSlotClick());
    }


    //Bool checks in the initial Click 
    private bool CanDeployTurret()
    {
        foreach (TurretSpawnPoint spawnPoint in _turretSpawnPoints)
        {
            if (!spawnPoint.HasTurret && spawnPoint.IsUnlocked)
            {
                return true;
            }
        }
        return false;
    }
    private bool CanAddSlot()
    {
        foreach (TurretSpawnPoint spawnPoint in _turretSpawnPoints)
        {
            if (!spawnPoint.IsUnlocked)
            {
                return true;
            }
        }
        return false;
    }


    //Courotine that waits for mouse Click
    private IEnumerator WaitForTurretClick()
    {
        while (_isWaitingForClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (TryGetEmptySlot(out var slot))
                {
                    AddTurretToEmptySlot(slot);
                    yield break;
                }
                else
                {
                    _isWaitingForClick = false;
                    yield break;
                }
            }
            yield return null;
        }
    }
    private IEnumerator WaitForSlotClick()
    {
        while (_isWaitingForClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var hit = MouseRayCaster.Instance.GetHit();

                if (TryGetAvailableSlot(out var slot))
                {
                    AddSlot(slot);
                    yield break;
                }
                else
                {
                    _isWaitingForClick = false;
                    HideAllAvailablePoints();
                    yield break;
                }
            }
            yield return null;
        }
    }


    //Bool checks that the mouse click brings back a TurretSpawnPoint component
    private bool TryGetEmptySlot(out TurretSpawnPoint validSlot)
    {
        var hit = MouseRayCaster.Instance.GetHit();

        if (hit.HasValue &&
            hit.Value.collider.TryGetComponent<TurretSpawnPoint>(out var slot) &&
            slot.IsUnlocked && 
            !slot.HasTurret)
        {
            validSlot = slot;
            return true;
        }

        validSlot = null;
        return false;
    }
    private bool TryGetAvailableSlot(out TurretSpawnPoint validSlot)
    {
        var hit = MouseRayCaster.Instance.GetHit();

        if (hit.HasValue &&
            hit.Value.collider.TryGetComponent<TurretSpawnPoint>(out var slot) &&
            !slot.IsUnlocked)
        {
            validSlot = slot;
            return true;
        }

        validSlot = null;
        return false;
    }
    private void GetReleventCheck()
    {
        switch (TurretbuttonType)
        {
            case TurretButtonType.DeployTurret:

                break;
            case TurretButtonType.AddSlot: 
                
                break;
            case TurretButtonType.SellTurret:

                break;
            default:
                Debug.LogWarning("Unknown TurretButtonType type: " + TurretbuttonType);
                break;
        }
    }


    //Actual logic to aplly when all the conditions are matching 
    private void AddTurretToEmptySlot(TurretSpawnPoint slot)
    {
        _isWaitingForClick = false;
        PlayerCurrency.Instance.SubtractMoney(_cost);
        _turretSpawnPos = slot.gameObject.transform;
        slot.HasTurret = true;
        StopFlashingAllAvailablePoints();
        Instantiate(_turretPrefab, _turretSpawnPos.position, _turretSpawnPos.rotation);

        //onTurretPlaced?.Invoke();
    }
    private void AddSlot(TurretSpawnPoint slot)
    {
        _isWaitingForClick = false;
        PlayerCurrency.Instance.SubtractMoney(_cost);
        slot.IsUnlocked = true;
        HideAllAvailablePoints();

        //onSlotUnlocked?.Invoke();
    }


    // highlight Logic
    private void FlashAllAvailablePoints()
    {
        foreach (var point in _turretSpawnPoints)
        {
            if (!point.HasTurret && point.IsUnlocked)
            {
                point.StartFlashing(0.2f);
            }
        }
    }
    private void StopFlashingAllAvailablePoints()
    {
        foreach (var point in _turretSpawnPoints)
        {
            point.StopFlashing();
        }
    }
    private void ShowAllAvailablePoints()
    {
        foreach (var point in _turretSpawnPoints)
        {
            if (!point.IsUnlocked)
            {
                point.ShowHighlight(true);
            }
        }
    }
    private void HideAllAvailablePoints()
    {
        foreach (var point in _turretSpawnPoints)
        {
            if (!point.IsUnlocked)
            {
                point.ShowHighlight(false);
            }
        }
    }









}



