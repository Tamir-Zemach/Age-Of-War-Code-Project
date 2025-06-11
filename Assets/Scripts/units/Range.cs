using UnityEngine;

[RequireComponent(typeof(UnitBaseBehaviour))]
public class Range : MonoBehaviour
{
    private UnitBaseBehaviour UnitBaseBehaviour;
    private Unit unit;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;

    [SerializeField] private GameObject _rangeTriggerArea;

    [SerializeField] private LayerMask _rangeTarget;
    public Collider[] hitTargets;
    private void Awake()
    {
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
        _bulletPrefab = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
        RangeBullet bulletScript = _bulletPrefab.GetComponent<RangeBullet>();

        if (bulletScript != null)
        {
            bulletScript.Initialize(hitTargets[0].transform); // Pass the target
        }
    }
    private void AttackCancel()
    {
    }

    private void CheckForTarget()
    {
        hitTargets = Physics.OverlapBox(
            _rangeTriggerArea.transform.position,
            _rangeTriggerArea.transform.localScale * 0.5f,
            _rangeTriggerArea.transform.rotation,
            _rangeTarget);

        if (hitTargets.Length > 0)
        {
            GameObject target = hitTargets[0].gameObject;
        }
    }
}
