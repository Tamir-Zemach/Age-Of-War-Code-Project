namespace Assets.Scripts
{
    using UnityEngine;
    using System.Collections;

    [RequireComponent(typeof(UnitBaseBehaviour))]

    public class Attacker : MonoBehaviour
    {
        private UnitBaseBehaviour UnitBaseBehaviour;
        private Unit unit;

        [SerializeField] private GameObject _attackTriggerArea;

        [SerializeField] private LayerMask _attackTarget;

        public Collider[] hitTargets;
        private void Awake()
        {
            _attackTriggerArea.SetActive(false);
            UnitBaseBehaviour = GetComponent<UnitBaseBehaviour>();
            unit = UnitBaseBehaviour.Unit;
            if (UnitBaseBehaviour != null)
            {
               UnitBaseBehaviour.OnAttack += Attack;
               UnitBaseBehaviour.OnAttackCancel += AttackCancel;
            }

        }

        private void Attack()
        {
            CheckForTarget();
            _attackTriggerArea.SetActive(true);

        }
        private void AttackCancel()
        {
            _attackTriggerArea.SetActive(false);
        }
        private void GiveDamage(GameObject target)
        {
            UnitHealthManager targetHealth = target.GetComponent<UnitHealthManager>();
            if (targetHealth != null)
            {
                targetHealth.GetHurt(unit._strength); 
            }
        }

        private void CheckForTarget()
        {
            hitTargets = Physics.OverlapBox(
                _attackTriggerArea.transform.position,
                _attackTriggerArea.transform.localScale * 0.5f, 
                _attackTriggerArea.transform.rotation,
                _attackTarget);

            if (hitTargets.Length > 0)
            {
                GameObject target = hitTargets[0].gameObject; // Only hit the first target
                GiveDamage(target);
            }
        }

        private void OnDrawGizmos()
        {
            if (_attackTriggerArea == null) return;

            Gizmos.color = Color.yellow;
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(
                _attackTriggerArea.transform.position,
                _attackTriggerArea.transform.rotation,
                Vector3.one
            );
            Gizmos.matrix = rotationMatrix;

            Gizmos.DrawWireCube(Vector3.zero, _attackTriggerArea.transform.localScale);
        }
    }
}