using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Character : MonoBehaviour
{

    public delegate void AttackDelegate();
    public event Action OnAttack; 
    public event Action OnAttackCancel;


    private NavMeshAgent _agent;
    private GameObject _enemyBase;

    [Header("Tags")]
    [Tooltip("The character will walk toward the GameObject with this tag:")]
    [SerializeField, TagSelector] private string _baseTag;

    [Tooltip("The character ")]
    [SerializeField, TagSelector] private string _friendlyCharacterTag;

    [Tooltip("The character will stop when it encounters the GameObject with this tag:")]
    [SerializeField, TagSelector] private string _enemyCharacterTag;


    [Header("Character Parameters")]
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _health = 1;
    [SerializeField] private float _insialAttackDelay = 1;
    [SerializeField] private float _attackPeriod = 0.2f;


    [Header("BoxCast Parameters")]
    [Tooltip("How far the character detects other characters")]
    [SerializeField] private float _raylength;

    [Tooltip("The size of the box the character casts")]
    [SerializeField] private Vector3 boxSize = new Vector3(0.5f, 0.5f, 0.5f);

    [Tooltip("The color of the box the character casts")]
    [SerializeField] private Color boxColor = Color.red;

    private bool _isAttacking = false;
    private Coroutine _currentCoroutine;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyBase = GameObject.FindGameObjectWithTag(_baseTag);
        _agent.destination = _enemyBase.transform.position;
        _agent.speed = _speed;
    }

    private void Update()
    {
        CheckSurroundings();
    }

    private void CheckSurroundings()
    {
        _agent.isStopped = false; // Default state

        if (Physics.BoxCast(transform.position, boxSize, transform.forward, out var hitInfo, Quaternion.identity, _raylength))
        {
            GameObject obj = hitInfo.transform.gameObject;

            if (obj.CompareTag(_enemyCharacterTag))
            {
                //Debug.Log($"{gameObject.name} is seeing {hitInfo.transform.gameObject.name}");
                _agent.isStopped = true;
                //Debug.Log($"{gameObject.name} is stopping? : {_agent.isStopped}");

                if (!_isAttacking)
                {
                    _isAttacking = true;
                    Attack();
                }
                return; // Early exit if enemy detected
            }

            if (obj.CompareTag(_friendlyCharacterTag))
            {
                NavMeshAgent otherAgent = obj.GetComponent<NavMeshAgent>();

                if (otherAgent != null && otherAgent.isStopped)
                {
                    _agent.isStopped = true;
                }
            }
        }

        if (_currentCoroutine != null && !_agent.isStopped)
        {
            StopAllCoroutines();
            _currentCoroutine = null;
            OnAttackCancel?.Invoke();
            _isAttacking = false;
        }
    }


    private void Attack()
    {
        _currentCoroutine = StartCoroutine(AttackAction(_insialAttackDelay, _attackPeriod));
    }

    IEnumerator AttackAction(float insialAttackDelay, float attackPeriod)
    {
        yield return new WaitForSeconds(insialAttackDelay);
        OnAttack?.Invoke();
        yield return new WaitForSeconds(attackPeriod);
        OnAttackCancel?.Invoke();
        _isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = boxColor;

        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        // Perform the BoxCast
        if (Physics.BoxCast(origin, boxSize , direction, out RaycastHit hitInfo, Quaternion.identity, _raylength))
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



