using UnityEngine;



[RequireComponent(typeof(Rigidbody))]
public class TurretBullet : MonoBehaviour
{
    private Rigidbody _rb;
    
    [Tooltip("Tag assigned to the opposite Unit")]
    [SerializeField, TagSelector] private string _oppositeUnitTag;
    [SerializeField] private float _destroyTime;
    private int _strength;
    private int _speed;
    private bool _hasHit;
    private float _timer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        
    }
    private void Start()
    {
        ApplyForceAtStart();
    }
    private void ApplyForceAtStart()
    {
        _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        //check for hit one time
        if (_hasHit) return;

        if (other.gameObject.CompareTag(_oppositeUnitTag))
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
            targetHealth.GetHurt(_strength);
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
    public void SetBulletParameters(int strength,int speed)
    {
        _strength = strength;
        _speed = speed;
    }
}
