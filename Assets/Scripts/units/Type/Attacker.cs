namespace Assets.Scripts
{
    using UnityEngine;

    [RequireComponent(typeof(UnitBaseBehaviour))]

    public class Attacker : MonoBehaviour
    {
        private UnitBaseBehaviour UnitBaseBehaviour;
        private Unit unit;

        private void Awake()
        {
            UnitBaseBehaviour = GetComponent<UnitBaseBehaviour>();
            unit = UnitBaseBehaviour.Unit;
            if (UnitBaseBehaviour != null)
            {
               UnitBaseBehaviour.OnAttack += Attack;
               UnitBaseBehaviour.OnBaseAttack += BaseAttack;
            }

        }

        private void Attack(GameObject target)
        {
            GiveDamage(target); 
        }
        private void BaseAttack(GameObject target)
        {
            GiveDamageToBase(target);  
        }
        private void GiveDamage(GameObject target)
        {
            UnitHealthManager targetHealth = target.GetComponent<UnitHealthManager>();
            if (targetHealth != null)
            {
                targetHealth.GetHurt(unit._strength); 
            }
        }
        private void GiveDamageToBase(GameObject target)
        {
            EnemyBaseHealthManger enemyBaseHealth = target.GetComponent<EnemyBaseHealthManger>();
            PlayerBaseHealthManager playerBaseHealth = target.GetComponent<PlayerBaseHealthManager>();

            if (enemyBaseHealth != null)
            {
                enemyBaseHealth.GetHurt(unit._strength);
            }
            else if (playerBaseHealth != null)
            {
                playerBaseHealth.GetHurt(unit._strength);
            }
        }


    }
}