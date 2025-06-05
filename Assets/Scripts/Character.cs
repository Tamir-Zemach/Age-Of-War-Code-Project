using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(NavMeshAgent))]
public class Character : MonoBehaviour
{
    private NavMeshAgent _agent;
    private GameObject _enemyBase;

    [Header("Tags")]
    [Tooltip("The character would walk towards the GameObject with this Tag:")]
    [SerializeField, TagSelector] private string _baseTag;

    [Tooltip("The character Would stop when it encounters the GameObject with this Tag:")]
    [SerializeField, TagSelector] private string _characterTag;

    [Header("Character Parameters")]
    [SerializeField] private float _speed;
    [SerializeField] private float _cost;
    [SerializeField] private float _health;

    [Header("RayCast Parameters")]
    [SerializeField] private float _raylength;
    [SerializeField] Vector3 boxSize = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] Color boxColor = Color.red;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyBase = GameObject.FindGameObjectWithTag(_baseTag);
        _agent.destination = _enemyBase.transform.position;
        _agent.speed = _speed;
    }

    private void Update()
    {
        StopingWhenSeeingEnemy();
    }

    public virtual void Attack()
    {
        Debug.Log("excute attack logic");
    }

    private void StopingWhenSeeingEnemy()
    {
        
        if (Physics.BoxCast(transform.position, boxSize, transform.forward, out var hitInfo, Quaternion.identity, _raylength))
        {
            if (hitInfo.transform.gameObject.CompareTag(_characterTag))
            {
                _agent.isStopped = true;
                transform.LookAt(hitInfo.transform.position);
                Attack();
            }
        }
        else
        {
            _agent.isStopped = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = boxColor;

        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        // Perform the BoxCast
        if (Physics.BoxCast(origin, boxSize / 2, direction, out RaycastHit hitInfo, Quaternion.identity, _raylength))
        {
            // Draw the hit box
            Gizmos.DrawWireCube(hitInfo.point, boxSize);
        }

        // Draw the initial box
        Gizmos.DrawWireCube(origin, boxSize);

        // Draw the movement path
        Gizmos.DrawLine(origin, origin + direction * _raylength);

    }
}
