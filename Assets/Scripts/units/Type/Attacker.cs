﻿namespace Assets.Scripts
{
    using Assets.Scripts.units.Behavior;
    using UnityEngine;

    [RequireComponent(typeof(UnitBaseBehaviour))]

    public class Attacker : MonoBehaviour
    {
        private UnitBaseBehaviour UnitBaseBehaviour;
        private UnitData unit;

        private void Awake()
        {
            UnitBaseBehaviour = GetComponent<UnitBaseBehaviour>();
            if (UnitBaseBehaviour != null)
            {
                UnitBaseBehaviour.OnAttack += Attack;
            }

        }
        private void OnDestroy()
        {
            if (UnitBaseBehaviour != null)
            {
                UnitBaseBehaviour.OnAttack -= Attack;
            }
        }
        private void Start()
        {
            unit = UnitBaseBehaviour.Unit;
        }

        public void Attack(GameObject target)
        {
            GiveDamage(target); 
        }
        private void GiveDamage(GameObject target)
        {
            (target.GetComponent<UnitHealthManager>() as IDamageable
             ?? target.GetComponent<EnemyBaseHealthManger>() as IDamageable
             ?? target.GetComponent<PlayerBaseHealthManager>() as IDamageable)
            ?.GetHurt(unit._strength);
        }

    }

}