
using Assets.Scripts.Enems;
using UnityEngine;


namespace Assets.Scripts.units
{
    [CreateAssetMenu(fileName = "TurretAndSpecialAttackLevelUpData", menuName = "TurretAndSpecialAttackLevelUpData", order = 5)]
    public class TurretAndSpecialAttackLevelUpData : ScriptableObject
    {
        [Tooltip("What age this upgrade belongs to")]
        public AgeStageType ageStage;

        [Tooltip("The Special attack Type this age should unlock")]
        public SpecialAttackType specialAttackType;

        public Sprite _SpecialAttackSprite;

        public Sprite _TurretSprite;

    }
}

