using Assets.Scripts.Enems;
using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

public class TestTurretButton : MonoBehaviour
{
    //events for later
    //[Header("Events")]
    //public UnityEvent onTurretPlaced;
    //public UnityEvent onSlotUnlocked;
    //public UnityEvent onTurretSelled;
    [Tooltip("The type of action this button performs (Deploy, AddSlot, Sell).")]
    [SerializeField] private TurretButtonType TurretbuttonType;

    [Tooltip("The turret prefab to instantiate when deploying.")]
    [SerializeField] private GameObject _turretPrefab;

    [Tooltip("The cost in currency to perform this turret action.")]
    [SerializeField] private int _cost;

    [Tooltip("The amount of money refunded when selling a turret.")]
    [SerializeField] private int _moneyToGiveBack;

    private Transform _turretSpawnPos;

    private bool _isWaitingForClick;

    [SerializeField] private List<TurretSpawnPoint> _turretSpawnPoints = new List<TurretSpawnPoint>();

    //System.Func Is for getting inmput and returning output - Asking Questions "can you check" 
    //System.Action is for only returning output - demand commend "just do it"
    private Dictionary<TurretButtonType, Func<TurretSpawnPoint, bool>> _conditions;

    private void Awake()
    {
        _conditions = new Dictionary<TurretButtonType, Func<TurretSpawnPoint, bool>>
    {
        { TurretButtonType.DeployTurret, spawnPoint => !spawnPoint.HasTurret && spawnPoint.IsUnlocked },
        { TurretButtonType.AddSlot, spawnPoint => !spawnPoint.IsUnlocked },
        { TurretButtonType.SellTurret, spawnPoint => spawnPoint.HasTurret }
    };
    }



    //The actual function to use onClick
    public void DeployTurret()
    {
        ExecuteTurretButtonAction(TurretButtonType.DeployTurret, 
            VisualFeedbackType.Flash,
            AddTurretToEmptySlotLogic, 
            () => PlayerCurrency.Instance.HasEnoughMoney(_cost) && CanDeployTurret());
    }

    public void AddTurretSlot()
    {
        ExecuteTurretButtonAction(TurretButtonType.AddSlot,
            VisualFeedbackType.Highlight,
            AddSlotLogic,
            () => PlayerCurrency.Instance.HasEnoughMoney(_cost) && CanAddSlot());
    }

    public void SellTurret()
    {
        ExecuteTurretButtonAction(TurretButtonType.SellTurret,
            VisualFeedbackType.Highlight,
            SellTurretLogic,
            HaveAnyTurrets);
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
    private bool HaveAnyTurrets()
    {
        foreach (TurretSpawnPoint spawnPoint in _turretSpawnPoints)
        {
            if (spawnPoint.HasTurret)
            {
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// Coroutine that listens for a left-click on a valid turret spawn point.
    /// If a valid slot is clicked, invokes the corresponding logic and clears visual feedback.
    /// If an invalid click occurs, cancels the action and also clears visuals.
    /// </summary>
    /// <param name="isValidSlot">A condition to validate the clicked TurretSpawnPoint.</param>
    /// <param name="onSlotSelected">The logic to execute if a valid slot is selected.</param>
    private IEnumerator WaitForSlotClick(Func<TurretSpawnPoint, bool> isValidSlot,
        Action<TurretSpawnPoint> onSlotSelected)
    {
        _isWaitingForClick = true;

        while (_isWaitingForClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var hit = MouseRayCaster.Instance.GetHit();

                if (hit.HasValue && hit.Value.collider.TryGetComponent<TurretSpawnPoint>(out var slot))
                {
                    if (isValidSlot(slot))
                    {
                        ConfirmSlotAndInvoke(slot, onSlotSelected);
                        yield break;
                    }
                }

                // If invalid click, still clear visuals
                _isWaitingForClick = false;
                SetVisualFeedback(_ => true, VisualFeedbackType.Off);
                yield break;
            }

            yield return null;
        }
    }
    private void ConfirmSlotAndInvoke(TurretSpawnPoint slot, Action<TurretSpawnPoint> callback)
    {
        _isWaitingForClick = false;
        SetVisualFeedback(_ => true, VisualFeedbackType.Off);
        callback?.Invoke(slot);
    }


    //Actual logic to aplly when all the conditions are matching 
    private void AddSlotLogic(TurretSpawnPoint slot)
    {
        PlayerCurrency.Instance.SubtractMoney(_cost);
        slot.IsUnlocked = true;
        MaintainHighlightOnEmptyUnlockedSlots();
        // onSlotUnlocked?.Invoke();
    }

    private void SellTurretLogic(TurretSpawnPoint slot)
    {
        PlayerCurrency.Instance.AddMoney(_moneyToGiveBack);
        slot.HasTurret = false;

        var turret = slot.GetComponentInChildren<TurretBaseBehavior>();
        if (turret != null)
        {
            Destroy(turret.gameObject);
        }

        MaintainHighlightOnEmptyUnlockedSlots();
        // onTurretSelled?.Invoke();
    }

    private void AddTurretToEmptySlotLogic(TurretSpawnPoint slot)
    {
        PlayerCurrency.Instance.SubtractMoney(_cost);
        _turretSpawnPos = slot.transform;
        slot.HasTurret = true;

        GameObject turretPrefab = Instantiate(_turretPrefab, _turretSpawnPos.position, _turretSpawnPos.rotation);
        turretPrefab.transform.parent = slot.transform;

        MaintainHighlightOnEmptyUnlockedSlots();
        // onTurretPlaced?.Invoke();
    }


    /// <summary>
    /// Applies visual feedback to turret spawn points based on a condition and feedback mode.
    /// </summary>
    /// <remarks>
    /// Use this method for short-term, interaction-based visuals (e.g., during Deploy, Sell, or AddSlot).
    /// For persistent highlighting after turret state changes (like placing or selling a turret),
    /// use <see cref="MaintainHighlightOnEmptyUnlockedSlots"/> to keep feedback in sync.
    /// </remarks>
    /// <param name="condition">Condition to filter which spawn points receive visual feedback.</param>
    /// <param name="mode">The type of visual feedback to apply (Highlight, Flash, or Off).</param>
    /// <param name="flashInterval">Optional interval for flashing mode.</param>
    private void SetVisualFeedback(Func<TurretSpawnPoint, bool> condition, 
        VisualFeedbackType mode, float flashInterval = 0.2f)
    {
        foreach (var point in _turretSpawnPoints)
        {
            if (condition(point))
            {
                switch (mode)
                {
                    case VisualFeedbackType.Highlight:
                        point.ShowHighlight(true);
                        break;
                    case VisualFeedbackType.Flash:
                        point.StartFlashing(flashInterval);
                        break;
                    case VisualFeedbackType.Off:
                        point.ShowHighlight(false);
                        point.StopFlashing();
                        break;
                }
            }
            else if (mode == VisualFeedbackType.Off)
            {
                point.ShowHighlight(false);
                point.StopFlashing();
            }
        }
    }

    /// <summary>
    /// Updates visual feedback to highlight all unlocked turret slots that do not currently hold a turret.
    /// Call this after any state change (e.g., adding or selling a turret) to keep UI feedback accurate.
    /// </summary>
    private void MaintainHighlightOnEmptyUnlockedSlots()
    {
        SetVisualFeedback(SpawnPoint => SpawnPoint.IsUnlocked && !SpawnPoint.HasTurret, VisualFeedbackType.Highlight);
    }




    /// Executes a turret action by verifying currency,
    /// applying visual feedback, and waiting for a valid spawn point click.
    private void ExecuteTurretButtonAction(
        TurretButtonType type,
        VisualFeedbackType feedback,
        Action<TurretSpawnPoint> logic,
        Func<bool> currencyCheck)
    {
        if (!currencyCheck()) return;

        var condition = _conditions[type];
        SetVisualFeedback(condition, feedback);
        StartCoroutine(WaitForSlotClick(condition, logic));
    }
}

