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
            }

        }

        private void Attack(GameObject target)
        {
            GiveDamage(target);  // Pass the received target directly
        }
        private void GiveDamage(GameObject target)
        {
            UnitHealthManager targetHealth = target.GetComponent<UnitHealthManager>();
            if (targetHealth != null)
            {
                targetHealth.GetHurt(unit._strength); 
            }
        }

    }
}