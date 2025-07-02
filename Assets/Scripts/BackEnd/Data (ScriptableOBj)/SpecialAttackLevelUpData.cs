
using Assets.Scripts.Enems;
using Assets.Scripts.InterFaces;
using UnityEngine;


namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "SpecialAttackLevelUpData", menuName = "SpecialAttackLevelUpData", order = 5)]
    public class SpecialAttackLevelUpData : ScriptableObject, IUpgradable<SpecialAttackType>
    {
        [Tooltip("The Special attack Type:")]
        [SerializeField] private SpecialAttackType specialAttackType;

        [SerializeField] private bool _isFriendly;

        [Tooltip("What age this upgrade belongs to")]
        [SerializeField] private AgeStageType _ageStage;

        public SpecialAttackType Type => specialAttackType;

        public bool IsFriendly => _isFriendly;

        public int AgeStage => (int)_ageStage;

        public Sprite SpecialAttackSprite;

        [SerializeField] private GameObject _unitPrefab;
        public GameObject Prefab => _unitPrefab;

        public void SetPrefab(GameObject prefab)
        {
            _unitPrefab = prefab;
        }
    }
}

