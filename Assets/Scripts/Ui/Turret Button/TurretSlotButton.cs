using Assets.Scripts;
using Assets.Scripts.turrets;
using UnityEngine;
using System.Collections;
public class TurretSlotButton : MonoBehaviour
{
    [SerializeField] private int slotCost;
    [SerializeField] private TurretSpawnPoint[] _spawnPoints;

    private bool isAwaitingPlacement;

    public void AddTurretSlot()
    {
        if (!PlayerCurrency.Instance.HasEnoughMoney(slotCost)) return;

        isAwaitingPlacement = true;
        ShowAllAvailablePoints();
        StartCoroutine(WaitForSlotClick());
    }

    private IEnumerator WaitForSlotClick()
    {
        //TODO: make sure a Specific turret slot gets selected and the other ones are not 
        while (isAwaitingPlacement)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var hit = MouseRayCaster.Instance.GetHit();

                if (hit.HasValue && hit.Value.collider.TryGetComponent<TurretSpawnPoint>(out var slot))
                {
                    isAwaitingPlacement = false;
                    PlayerCurrency.Instance.SubtractMoney(slotCost);
                    slot.IsUnlocked = true;
                }
                else
                {
                    Debug.Log("Invalid slot or no hit.");
                    isAwaitingPlacement = false; // Or leave true to let the player try again
                    HideAllAvailablePoints();
                }
            }
            yield return null;
        }
    }



    private void ShowAllAvailablePoints()
    {
        foreach (var point in _spawnPoints)
        {
            bool isAvailable = !point.IsUnlocked;
            point.ShowHighlight(isAvailable);
        }
    }
    private void HideAllAvailablePoints()
    {
        foreach (var point in _spawnPoints)
        {
            point.ShowHighlight(false);
        }
    }
}