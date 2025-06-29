
using Assets.Scripts.Enems;
using UnityEngine;


namespace Assets.Scripts.units
{
    [CreateAssetMenu(fileName = "LevelUpData", menuName = "LevelUpData", order = 3)]
    public class UnitLevelUpData : ScriptableObject
    {
        [Tooltip("What age this upgrade belongs to")]
        public AgeStageType ageStage;

        [Tooltip("Is The Unit Friendly or an Enemy")]
        public bool isFriendly;

        [Tooltip("The Unit Type:")]
        public UnitType unitType;

        [Tooltip("The prefab to instansiate when deplyed")]
        public GameObject _unitPrefab;

        public Sprite _unitSprite;


        [Header("Enemy Unit Properties")]
        [Tooltip("How much money the player gains when this Unit is Destroyed")]
        public int _moneyWhenKilled;

        [Header("Unit Parameters")]
        [Tooltip("How far the unit detects opposite unit")]
        public int _range;
        [Tooltip("The speed Of the Unit")]
        public float _speed = 1;
        [Tooltip("The Health Of the Unit")]
        public int _health = 1;
        [Tooltip("How much every Hit will give damage")]
        public int _strength = 1;
        [Tooltip("The amount of time before a Unit Attacks (when lower its faster)")]
        public float _initialAttackDelay = 1;

    }
}

