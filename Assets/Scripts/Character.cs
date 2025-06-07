using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

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

    [Tooltip("The character will stop when it encounters the GameObject with this tag:")]
    [SerializeField, TagSelector] private string _characterTag;

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
        StopingWhenSeeingEnemy();
    }


    private void StopingWhenSeeingEnemy()
    {
        if (Physics.BoxCast(transform.position, boxSize, transform.forward, out var hitInfo, Quaternion.identity, _raylength))
        {
            if (hitInfo.transform.gameObject.CompareTag(_characterTag))
            {
                _agent.isStopped = true;

                if (!_isAttacking)
                {
                    _isAttacking = true;
                    Attack();
                }
            }
        }
        else
        {
            _agent.isStopped = false;
            if (_currentCoroutine != null)
            {
                StopAllCoroutines();
                _currentCoroutine = null;
                OnAttackCancel?.Invoke();
                _isAttacking = false;
            }
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



//private void StopingWhenSeeingEnemy()
//{
//    if (Physics.BoxCast(transform.position, boxSize, transform.forward, out var hitInfo, Quaternion.identity, _raylength))
//    {
//        if (hitInfo.transform.gameObject.CompareTag(_characterTag))
//        {
//            _agent.isStopped = true;

//            if (!isAttacking)
//            {
//                isAttacking = true;
//                Attack();
//            }
//        }
//    }
//    else
//    {
//        _agent.isStopped = false;
//        if (_currentCoroutine != null)
//        {
//            StopCoroutine(_currentCoroutine);
//            _currentCoroutine = null;
//        }
//    }
//}


//private void StopingWhenSeeingEnemy()
//{
//    if (!Physics.BoxCast(transform.position, boxSize, transform.forward, out var hitInfo, Quaternion.identity, _raylength))
//    {
//        _agent.isStopped = false;
//        StopRunningCoroutine();
//        return;
//    }

//    if (!hitInfo.transform.gameObject.CompareTag(_characterTag))
//        return;

//    _agent.isStopped = true;

//    if (isAttacking)
//        return;

//    isAttacking = true;
//    Attack();
//}

//private void StopRunningCoroutine()
//{
//    if (_currentCoroutine == null)
//        return;

//    StopCoroutine(_currentCoroutine);
//    _currentCoroutine = null;
//}