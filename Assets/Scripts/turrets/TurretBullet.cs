using Assets.Scripts.turrets;
using UnityEngine;



[RequireComponent(typeof(Rigidbody))]
public class TurretBullet : MonoBehaviour
{
    private Rigidbody _rb;
    
    [SerializeField] private float _destroyTime;
    private bool _hasHit;
    private float _timer;
    private TurretData _turretData;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _turretData = GameManager.Instance.GetTurretData();
        
    }
    private void Start()
    {
        ApplyForceAtStart();
    }
    private void ApplyForceAtStart()
    {
        _rb.AddForce(transform.forward * _turretData.BulletSpeed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        //check for hit one time
        if (_hasHit) return;

        if (other.gameObject.CompareTag(_turretData.OppositeUnitTag))
        {
            _hasHit = true;
            GiveDamage(other.gameObject);
            Destroy(gameObject);
        }
    }
    private void GiveDamage(GameObject target)
    {
        UnitHealthManager targetHealth = target.GetComponent<UnitHealthManager>();
        if (targetHealth != null)
        {
            targetHealth.GetHurt(_turretData.BulletStrength);
        }
    }
    private void Update()
    {
        DestroyAfterCertainTime();
    }
    private void DestroyAfterCertainTime()
    {
        _timer += Time.deltaTime;
        if (_timer >= _destroyTime)
        {
            Destroy(gameObject);
        }
    }

}
