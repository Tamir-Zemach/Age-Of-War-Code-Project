using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class RangeBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float arcHeight = 2f;
    private Rigidbody rb;
    private Transform target;

    public void Initialize(Transform target)
    {
        this.target = target;
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = new Vector3(direction.x * speed, arcHeight, direction.z * speed);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(target.tag))
        {
            GiveDamage(collision.gameObject);
            //Destroy(gameObject);
        }
    }


    private void GiveDamage(GameObject target)
    {
        UnitHealthManager targetHealth = target.GetComponent<UnitHealthManager>();
        if (targetHealth != null)
        {
            targetHealth.GetHurt(10);
        }
    }

}
