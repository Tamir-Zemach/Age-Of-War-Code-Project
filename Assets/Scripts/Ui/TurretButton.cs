using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.turrets;
using UnityEngine;

public class TurretButton : MonoBehaviour
{
    [SerializeField] private GameObject _turretPrefab;
    [SerializeField] private int _turretCost;

    private Transform _turretSpawnPos;

    [SerializeField] private List<TurretSpawnPoint> turretSpawnPoints = new List<TurretSpawnPoint>();
    public void DeployTurret()
    {
        if (PlayerCurrency.Instance.HasEnoughMoney(_turretCost) && CanDeployTurret())
        {
            PlayerCurrency.Instance.SubtractMoney(_turretCost);
            _turretSpawnPos = GetAvailableSpawnPoint().transform;
            Instantiate(_turretPrefab, _turretSpawnPos.position, _turretSpawnPos.rotation);
        }
    }

    private bool CanDeployTurret()
    {
        foreach (TurretSpawnPoint spawnPoint in turretSpawnPoints)
        {
            if (!spawnPoint.hasTurret)
            {
                return true; 
            }
        }
        return false; 
    }
    private TurretSpawnPoint GetAvailableSpawnPoint()
    {
        foreach (TurretSpawnPoint spawnPoint in turretSpawnPoints)
        {
            if (!spawnPoint.hasTurret)
            {
                return spawnPoint;
            }
        }
        return null;
    }
    public void DeployTurretToFirstEmptyPoint()
    {
        TurretSpawnPoint availablePoint = GetAvailableSpawnPoint();

        if (availablePoint != null && PlayerCurrency.Instance.HasEnoughMoney(_turretCost))
        {
            PlayerCurrency.Instance.SubtractMoney(_turretCost);

            Instantiate(_turretPrefab, availablePoint.transform.position, availablePoint.transform.rotation);
            availablePoint.hasTurret = true;
        }
    }
}
