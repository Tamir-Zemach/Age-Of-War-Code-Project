using Assets.Scripts.Enems;
using UnityEngine;



namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "TurretLevelUpData", menuName = "TurretLevelUpData", order = 6)]
    public class TurretLevelUpData : ScriptableObject
    {
        [Tooltip("What age this upgrade belongs to")]
        public AgeStageType ageStage;

        public TurretType TurretType;

        public Sprite TurretSprite;

        public GameObject TurretPrefab;

    }
}