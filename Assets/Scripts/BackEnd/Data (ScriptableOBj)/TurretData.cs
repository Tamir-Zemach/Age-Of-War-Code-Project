
using Assets.Scripts.Enems;
using Assets.Scripts.InterFaces;
using UnityEngine;

namespace Assets.Scripts.turrets
{
    [CreateAssetMenu(fileName = "Data", menuName = "TurretData", order = 2)]
    public class TurretData : ScriptableObject, IUpgradable<TurretType>
    {
        [Tooltip("The Turret Type:")]
        [SerializeField] private TurretType _turretType;

        [SerializeField] private bool _isFriendly;


        private AgeStageType _stageType;
        public int AgeStage => (int)_stageType;

        public TurretType Type => _turretType;

        public bool IsFriendly => _isFriendly;

        [Tooltip("Turret prefab")]
        [SerializeField] private GameObject _unitPrefab;
        public GameObject Prefab => _unitPrefab;

        public void SetPrefab(GameObject prefab)
        {
            _unitPrefab = prefab;
        }


        [Header("Detection Settings")]
        [Tooltip("Layer that contains opposite units.")]
        public LayerMask OppositeUnitLayer;

        [Tooltip("Tag assigned to the opposite Unit")]
        [TagSelector] public string OppositeUnitTag;

        [Tooltip("Tag assigned to the friendly base GameObject.")]
        [TagSelector] public string FriendlyBase;

        [Tooltip("How far the turret detects opposite Unit")]
        public float Range;

        [Tooltip("Size of the BoxCast.")]
        public Vector3 BoxSize = Vector3.one;

        [Header("Attack Settings")]
        [Tooltip("Projectile prefab to spawn when attacking.")]
        public GameObject BulletPrefab;

        [Tooltip("Delay before the turret fires its first shot after spotting an enemy.")]
        public float InitialAttackDelay;

        [Tooltip("How much damage each bullet inflicts.")]
        public int BulletStrength;

        [Tooltip("How much force will be added to the bullet at the start.")]
        public int BulletSpeed;

    }
}
