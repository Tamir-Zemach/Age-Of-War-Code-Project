using UnityEngine;

[RequireComponent(typeof(UnitBaseBehaviour))]
public class Range : MonoBehaviour
{
    private UnitBaseBehaviour UnitBaseBehaviour;
    private Unit unit;
    [SerializeField] private GameObject _bulletPrefab;
    private GameObject _bulletInctance;
    [SerializeField] private Transform _bulletSpawnPoint;

    RangeBullet bulletScript;
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
        _bulletInctance = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
        bulletScript = _bulletInctance.GetComponent<RangeBullet>();

        if (bulletScript != null)
        {
            bulletScript.Initialize(target.transform, unit._strength); 
            bulletScript = null;
        }
    }
    private void BaseAttack(GameObject target)
    {
        _bulletInctance = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
        bulletScript = _bulletInctance.GetComponent<RangeBullet>();

        if (bulletScript != null)
        {
            bulletScript.Initialize(target.transform, unit._strength);
            bulletScript = null;
        }
    }

}
