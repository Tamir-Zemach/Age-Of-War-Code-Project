
using Assets.Scripts.Enems;
using UnityEngine;


namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "SpecialAttackLevelUpData", menuName = "SpecialAttackLevelUpData", order = 5)]
    public class SpecialAttackLevelUpData : ScriptableObject
    {
        [Tooltip("What age this upgrade belongs to")]
        public AgeStageType ageStage;

        [Tooltip("The Special attack Type this age should unlock")]
        public SpecialAttackType specialAttackType;

        public Sprite SpecialAttackSprite;

        public GameObject SpecialAttackPrefab;
    }
}

