using Assets.Scripts.Enems;
using Assets.Scripts.InterFaces;
using UnityEngine;



namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "TurretLevelUpData", menuName = "TurretLevelUpData", order = 6)]
    public class TurretLevelUpData : ScriptableObject, IUpgradable<TurretType>
    {
        [Tooltip("The Turret Type:")]
        [SerializeField] private TurretType _turretType;

        [SerializeField] private bool _isFriendly;

        [Tooltip("What age this upgrade belongs to")]
        [SerializeField] private AgeStageType _ageStage;

        public TurretType Type => _turretType;

        public bool IsFriendly => _isFriendly;

        public int AgeStage => (int)_ageStage;

        public Sprite TurretSprite;

        [SerializeField] private GameObject _unitPrefab;
        public GameObject Prefab => _unitPrefab;

        public void SetPrefab(GameObject prefab)
        {
            _unitPrefab = prefab;
        }

    }
}