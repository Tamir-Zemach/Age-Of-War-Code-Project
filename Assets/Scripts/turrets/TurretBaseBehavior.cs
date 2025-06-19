using System.Collections;
using UnityEngine;

public class TurretBaseBehavior : MonoBehaviour
{
    [Header("Detection Settings")]
    [Tooltip("Layer that contains units considered enemies.")]
    [SerializeField] private LayerMask _oppositeUnitLayer;

    [Tooltip("Tag assigned to the friendly base GameObject.")]
    [SerializeField, TagSelector] private string _friendlyBase;

    [Tooltip("Origin point from which the BoxCast is performed.")]
    [SerializeField] private Transform _boxCastOrigin;

    [Tooltip("How far the turret detect oppsite Unit")]
    [SerializeField] private float _range;

    [Tooltip("Size of the BoxCast.")]
    [SerializeField] private Vector3 _boxSize = Vector3.one;

    [Header("Attack Settings")]
    [Tooltip("Delay before the turret fires its first shot after spotting an enemy.")]
    [SerializeField] private float _insialAttackDelay;

    [Tooltip("How much damage each bullet inflicts.")]
    [SerializeField] private int _bulletStrength;

    [Tooltip("How much force will be added to the bullet at the start.")]
    [SerializeField] private int _bulletSpeed;

    [Tooltip("Projectile prefab to spawn when attacking.")]
    [SerializeField] private GameObject _bulletPrefab;

    [Tooltip("Transform from where bullets will be instantiated.")]
    [SerializeField] private Transform _bulletSpawnPos;

    private Vector3 _origin;
    private Vector3 _direction;
    private bool _isAttacking;
    private Coroutine _currentCoroutine;

    public Vector3 BoxSize => _boxSize;
    public Vector3 Origin => _origin;
    public Vector3 Direction => _direction;
    public Transform BoxCastOrigin => _boxCastOrigin;
    public float Range => _range;
    public LayerMask OppositeUnitLayer => _oppositeUnitLayer;

    private void OnValidate()
    {
        _boxCastOrigin = GameObject.FindGameObjectWithTag(_friendlyBase).GetComponent<Transform>();
        _origin = _boxCastOrigin.position;
        _direction = _boxCastOrigin.forward;
    }

    private void Awake()
    {
        _boxCastOrigin = GameObject.FindGameObjectWithTag(_friendlyBase).GetComponent<Transform>();
        _origin = _boxCastOrigin.position;
        _direction = _boxCastOrigin.forward;
    }

    private void Update()
    {
        CheckForEnemys();
    }

    private void CheckForEnemys()
    {
        if (Physics.BoxCast(_origin, _boxSize, _direction, out var hitInfo,
            _boxCastOrigin.rotation, _range, _oppositeUnitLayer))
        {
            gameObject.transform.LookAt(hitInfo.transform);
            if (!_isAttacking)
            {
                Attack();
            }
        }
        else
        {
            ResetAttackStateIfNeeded();
        }  
    }

    private void Attack()
    {
        _isAttacking = true;
        _currentCoroutine = StartCoroutine(AttackLoop(_insialAttackDelay));
    }

    IEnumerator AttackLoop(float insialAttackDelay)
    {
        yield return new WaitForSeconds(insialAttackDelay);
        InstantiateBulletAndSetStrength();
        _isAttacking = false;
    }

    private void InstantiateBulletAndSetStrength()
    {
        GameObject bulletReference = Instantiate(_bulletPrefab, _bulletSpawnPos.position, _bulletSpawnPos.rotation);

        TurretBullet bulletScript = bulletReference.GetComponent<TurretBullet>();
        if (bulletScript != null)
        {
            bulletScript.SetBulletParameters(_bulletStrength, _bulletSpeed);
        }
    }

    private void ResetAttackStateIfNeeded()
    {
        if (_currentCoroutine != null)
        {
            StopAllCoroutines();
            _currentCoroutine = null;
            _isAttacking = false;
        }
    }
}