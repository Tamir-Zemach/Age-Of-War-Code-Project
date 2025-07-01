
using Assets.Scripts.Enems;
using UnityEngine;

//TODO: Upgrade the code - make it like the TurretData - Same for its buttons 

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "SpecialAttackData", menuName = "SpecialAttackData", order = 4)]
    public class SpecialAttackData : ScriptableObject
    {

        public Sprite _sprite;

        [Tooltip("What age this upgrade belongs to")]
        public AgeStageType ageStage;

        [Tooltip("The Special attack Type:")]
        public SpecialAttackType specialAttackType;

        [Tooltip("The prefab to instansiate when deplyed")]
        public GameObject _specialAttckPrefab;


        public int _specialAttackCost;


    }

}