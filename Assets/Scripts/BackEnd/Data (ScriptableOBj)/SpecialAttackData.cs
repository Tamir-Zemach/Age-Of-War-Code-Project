
using Assets.Scripts.Enems;
using Assets.Scripts.InterFaces;
using UnityEngine;

//TODO: Upgrade the code - make it like the TurretData - Same for its buttons 

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "SpecialAttackData", menuName = "SpecialAttackData", order = 4)]
    public class SpecialAttackData : ScriptableObject ,IUpgradable<SpecialAttackType>
    {
        [Tooltip("The Special attack Type:")]
        [SerializeField] private SpecialAttackType specialAttackType;

        [SerializeField] private bool _isFriendly;

        private AgeStageType _stageType;
        public SpecialAttackType Type => specialAttackType;

        public bool IsFriendly => _isFriendly;

        [Tooltip("The prefab to instansiate when deplyed")]
        [SerializeField] private GameObject _unitPrefab;
        public GameObject Prefab => _unitPrefab;

        public void SetPrefab(GameObject prefab)
        {
            _unitPrefab = prefab;
        }

        public int AgeStage => (int)_stageType;

        public Sprite _sprite;

        [Tooltip("What age this upgrade belongs to")]
        [SerializeField] private AgeStageType ageStage;

        public int _specialAttackCost;


    }

}