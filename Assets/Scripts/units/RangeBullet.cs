using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class RangeBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float arcHeight = 2f;
    private Rigidbody rb;
    private Transform target;
    private int _strength;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Initialize(Transform target, int strength)
    {
        this.target = target;
        _strength = strength;
        if (rb != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = new Vector3(direction.x * speed, arcHeight, direction.z * speed);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (target != null && collision.gameObject.CompareTag(target.tag))
        {
            GiveDamage(collision.gameObject);
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

}
