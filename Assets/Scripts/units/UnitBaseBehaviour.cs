using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitBaseBehaviour : MonoBehaviour
{

    public delegate void AttackDelegate();
    public event Action OnAttack; 
    public event Action OnAttackCancel;


    private NavMeshAgent _agent;
    private GameObject _enemyBase;

    [Tooltip("The scriptable object that have all of the unit parameters")]
    public Unit Unit;

    [Tooltip("The size of the box the character casts")]
    [SerializeField] private Vector3 boxSize = new Vector3(0.5f, 0.5f, 0.5f);

    [Tooltip("The color of the box the character casts")]
    [SerializeField] private Color boxColor = Color.red;

    private bool _isAttacking = false;
    private Coroutine _currentCoroutine;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyBase = GameObject.FindGameObjectWithTag(Unit._baseTag);
        _agent.destination = _enemyBase.transform.position;
        _agent.speed = Unit._speed;
    }

    private void Update()
    {
        CheckSurroundings();
    }

   
    private void CheckSurroundings()
    {
        _agent.isStopped = false; // Default state

        if (Physics.BoxCast(transform.position, boxSize, transform.forward, out var hitInfo, Quaternion.identity, Unit._raylength))
        {
            GameObject obj = hitInfo.transform.gameObject;

            if (obj.CompareTag(Unit._enemyCharacterTag))
            {
                _agent.isStopped = true;

                if (!_isAttacking)
                {
                    _isAttacking = true;
                    Attack();
                }
                return; // Early exit if enemy detected
            }

            if (obj.CompareTag(Unit._friendlyCharacterTag))
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
        _currentCoroutine = StartCoroutine(AttackAction(Unit._insialAttackDelay, Unit._attackPeriod));
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
        if (Physics.BoxCast(origin, boxSize , direction, out RaycastHit hitInfo, Quaternion.identity, Unit._raylength))
        {
            // Draw the hit box
            Gizmos.DrawWireCube(hitInfo.point, boxSize);
        }

        // Draw the initial box
        Gizmos.DrawWireCube(origin, boxSize);

        // Draw the movement path
        Gizmos.DrawLine(origin, origin + direction * Unit._raylength);

    }




}



