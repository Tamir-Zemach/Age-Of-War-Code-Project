
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class UnitBaseBehaviour : MonoBehaviour
{
    public delegate void AttackDelegate(GameObject target);
    public event AttackDelegate OnAttack;
    public event Action OnInitialized;

    private NavMeshAgent _agent;
    private UnitHealthManager HealthManager;
    private GameObject _enemyBase;
    private Coroutine _currentCoroutine;
    private Collider _col;

    private bool _isAttacking = false;
    public bool IsAttacking => _isAttacking;

    private bool _isDying;

    private GameObject _currentTarget;

    private Animator Animator;

    public UnitData Unit { get; private set; }

    public void Initialize(UnitData unitData)
    {
        Unit = unitData;
        _agent = GetComponent<NavMeshAgent>();
        HealthManager = GetComponent<UnitHealthManager>();
        HealthManager.OnDying += Dying;
        _enemyBase = GameObject.FindGameObjectWithTag(Unit._oppositeBaseTag);
        _agent.destination = _enemyBase.transform.position;
        _agent.speed = Unit._speed;
        _col = GetComponent<Collider>();
        Animator = GetComponentInChildren<Animator>();
        OnInitialized?.Invoke();
    }

    private void Update()
    {
        if (!_isDying)
        {
            CheckForEnemyUnit();
            CheckForFriendlyUnit();
        }
    }


    private void CheckForEnemyUnit()
    {
        _agent.isStopped = false; // Default state

        if (Physics.BoxCast(transform.position, Unit.boxSize, transform.forward, out var hitInfo, Quaternion.identity, Unit._range, Unit._enemyCharacterMask))
        {
            GameObject obj = hitInfo.transform.gameObject;

            if (obj.CompareTag(Unit._oppositeUnitTag))
            {
                HandleEnemyDetection(obj, false);
            }
            if (obj.CompareTag(Unit._oppositeBaseTag))
            {
                HandleEnemyDetection(obj, true);
            }
        }

        ResetAttackStateIfNeeded();
    }
    private void HandleEnemyDetection(GameObject target, bool isBase)
    {
        _agent.isStopped = true;
        if (!_isAttacking)
        {
            _isAttacking = true;
                Attack(target);
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
        _currentTarget = target;
        _currentCoroutine = StartCoroutine(AttackAction(Unit._initialAttackDelay, target));
    }

    public GameObject GetAttackTarget() => _currentTarget;

    IEnumerator AttackAction(float initialAttackDelay, GameObject target)
    {
        yield return new WaitForSeconds(initialAttackDelay);
        if (Animator == null)
        {
            OnAttack?.Invoke(target);
        }
        // No longer calling OnAttack here
        _isAttacking = false;
    }

    private void Dying()
    {
        _isDying = true;
        _agent.isStopped = true;
        _col.enabled = false;

    }

    private void OnDrawGizmos()
    {
        if (Unit == null) return;
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



