using Assets.Scripts.Enems;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Assets.Scripts.Ui.TurretButton
{
    public partial class TurretButton : MonoBehaviour
    {
        [Tooltip("The type of action this button triggers.")]
        [SerializeField] private TurretButtonType TurretButtonType;

        [Tooltip("Prefab to spawn when deploying a turret.")]
        [SerializeField] private GameObject _turretPrefab;

        [Tooltip("Cost for triggering this action.")]
        [SerializeField] private int _cost;

        [Tooltip("Refund granted when selling a turret.")]
        [SerializeField] private int _moneyToGiveBack;

        [Tooltip("Parent GameObject that contains all TurretSpawnPoint children.")]
        [SerializeField] private GameObject _turretSpawnPointsParent;

        [HideInInspector] private List<TurretSpawnPoint> _turretSpawnPoints = new();

        private Transform _turretSpawnPos;
        private bool _isWaitingForClick;

        private Dictionary<TurretButtonType, Func<TurretSpawnPoint, bool>> _conditions;

    

        private void Awake()
        {
            _conditions = new Dictionary<TurretButtonType, Func<TurretSpawnPoint, bool>>
            {
               { TurretButtonType.DeployTurret, spawnPoint => !spawnPoint.HasTurret && spawnPoint.IsUnlocked },
               { TurretButtonType.AddSlot, spawnPoint => !spawnPoint.IsUnlocked },
               { TurretButtonType.SellTurret, spawnPoint => spawnPoint.HasTurret }
            };
            GetAllFriendlyTurretSpawnPoints();
        }
        private void GetAllFriendlyTurretSpawnPoints()
        {
            _turretSpawnPoints.Clear();

            if (_turretSpawnPointsParent == null)
            {
                Debug.LogWarning($"{nameof(TurretButton)}: Turret spawn points parent is not assigned.");
                return;
            }

            _turretSpawnPoints.AddRange(_turretSpawnPointsParent.GetComponentsInChildren<TurretSpawnPoint>());
        }

#if UNITY_EDITOR
        public static class FieldNames
        {
            public const string TurretType = nameof(TurretButtonType);
            public const string Prefab = nameof(_turretPrefab);
            public const string Cost = nameof(_cost);
            public const string Refund = nameof(_moneyToGiveBack);
            public const string SpawnPointParent = nameof(_turretSpawnPointsParent);
        }
#endif
    }
}
