using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class RangeBullet : MonoBehaviour
{
    
    [Tooltip("The Bullet will get destroy after this amount of time:")]
    [SerializeField] private float _destroyTime;

    [Tooltip("The Bullet will get stuck on the GameObject with the tag:")]
    [SerializeField, TagSelector] private string _groundTag;

    [Tooltip("The Base which can be attacked by the bullet:")]
    [SerializeField, TagSelector] private string _oppositeBase;

    [Tooltip("The opposite unit that the bullet can hurt")]
    [SerializeField, TagSelector] private string _oppositeUnit;

    private Rigidbody rb;
    private Transform target;
    private int _strength;
    private float _timer;
    private float _arcHeight = 1.5f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Initialize(Transform target, int strength)
    {
        this.target = target;
        _strength = strength;

        if (rb != null && target != null)
        {
            //physics logic - i need deaper explantion

            Vector3 startPos = transform.position;
            Vector3 targetPos = target.position;

            // Compute horizontal displacement (ignoring Y)
            Vector3 horizontalDirection = new Vector3(targetPos.x - startPos.x, 0, targetPos.z - startPos.z);

            // Calculate flight time based on gravity and arc height
            float gravity = Mathf.Abs(Physics.gravity.y);
            float timeToPeak = Mathf.Sqrt((2 * _arcHeight) / gravity); // Time to peak arc
            float totalFlightTime = timeToPeak * 2; // Full projectile time estimation

            // Adjust horizontal velocity based on total flight time
            Vector3 horizontalVelocity = horizontalDirection / totalFlightTime;

            // Set the initial vertical velocity to reach the arc height
            float verticalVelocity = Mathf.Sqrt(2 * gravity * _arcHeight);

            // Combine the forces
            Vector3 launchVelocity = horizontalVelocity + Vector3.up * verticalVelocity;

            // Apply force
            rb.useGravity = true;
            rb.AddForce(launchVelocity * rb.mass, ForceMode.Impulse);
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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(_oppositeUnit))
        {
            GiveDamage(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag(_groundTag))
        {
            rb.isKinematic = true;
        }
        if (collision.gameObject.CompareTag(_oppositeBase))
        {
            GiveDamageToBase(collision.gameObject);
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
    private void GiveDamageToBase(GameObject target)
    {
        EnemyBaseHealthManger enemyBaseHealth = target.GetComponent<EnemyBaseHealthManger>();
        PlayerBaseHealthManager playerBaseHealth = target.GetComponent<PlayerBaseHealthManager>();

        if (enemyBaseHealth != null)
        {
            enemyBaseHealth.GetHurt(_strength);
        }
        else if (playerBaseHealth != null)
        {
            playerBaseHealth.GetHurt(_strength);
        }
    }

}



