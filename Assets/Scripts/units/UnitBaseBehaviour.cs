
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitBaseBehaviour : MonoBehaviour
{
    public delegate void AttackDelegate(GameObject target);
    public event AttackDelegate OnAttack;

    private NavMeshAgent _agent;
    private GameObject _enemyBase;

    [Tooltip("The scriptable object that have all of the unit parameters")]
    public Unit Unit;

    private bool _isAttacking = false;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyBase = GameObject.FindGameObjectWithTag(Unit._oppositeBaseTag);
        _agent.destination = _enemyBase.transform.position;
        _agent.speed = Unit._speed;
    }

    private void Update()
    {
        CheckForEnemyUnit();
        CheckForFriendlyUnit();
    }


    private void CheckForEnemyUnit()
    {
        _agent.isStopped = false; // Default state

        if (Physics.BoxCast(transform.position, Unit.boxSize, transform.forward, out var hitInfo, Quaternion.identity, Unit._range, Unit._enemyCharacterMask))
        {
            GameObject obj = hitInfo.transform.gameObject;

            if (obj.CompareTag(Unit._oppositeUnitTag))
            {
                HandleEnemyDetection(obj); 
            }
        }

        ResetAttackStateIfNeeded();
    }
    private void HandleEnemyDetection(GameObject target)
    {
        _agent.isStopped = true;
        if (!_isAttacking)
        {
            _isAttacking = true;
            Attack(target); // Pass the detected enemy
        }
    }

    private void CheckForFriendlyUnit()
    {
        if (Physics.BoxCast(transform.position, Unit.boxSize, transform.forward, out var hitInfo, Quaternion.identity, Unit._rayLengthForFriendlyUnit))
        {
            GameObject obj = hitInfo.transform.gameObject;
            if (obj.CompareTag(Unit._friendlyUnitTag))
            {
                _agent.isStopped = true;
            }
        }
    }


    private void ResetAttackStateIfNeeded()
    {
        if (_currentCoroutine != null && !_agent.isStopped)
        {
            StopAllCoroutines();
            _currentCoroutine = null;
            _isAttacking = false;
        }
    }
    private void Attack(GameObject target)
    {
        _currentCoroutine = StartCoroutine(AttackAction(Unit._insialAttackDelay, target));
    }

    IEnumerator AttackAction(float insialAttackDelay, GameObject target)
    {
        yield return new WaitForSeconds(insialAttackDelay);
        OnAttack?.Invoke(target);
        _isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Unit.boxColor;

        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        // Perform the BoxCast
        if (Physics.BoxCast(origin,Unit.boxSize , direction, out RaycastHit hitInfo, Quaternion.identity, Unit._range))
        {
            // Draw the hit box
            Gizmos.DrawWireCube(hitInfo.point, Unit.boxSize);
        }

        // Draw the initial box
        Gizmos.DrawWireCube(origin, Unit.boxSize);

        // Draw the movement path
        Gizmos.DrawLine(origin, origin + direction * Unit._range);

    }




}



