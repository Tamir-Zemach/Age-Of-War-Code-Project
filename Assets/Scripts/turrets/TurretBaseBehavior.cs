using System.Collections;
using Assets.Scripts.turrets;
using UnityEngine;

public class TurretBaseBehavior : MonoBehaviour
{

    //"Origin point from which the BoxCast is performed
    private Transform _DetectionOrigin;

    [Tooltip("Transform from where bullets will be instantiated.")]
    [SerializeField] private Transform _bulletSpawnPos;

    private Vector3 _origin;
    private Vector3 _direction;
    private Quaternion _rotation;
    private bool _isAttacking;
    private Coroutine _currentCoroutine;
    private TurretData _turretData;

    public Vector3 Origin => _origin;
    public Vector3 Direction => _direction;

    public Quaternion Rotation => _rotation;
    private void OnValidate()
    {
        if (Application.isPlaying) return;
        if (GameManager.Instance != null)
        {
            GetData();
        }
    }

    private void Awake()
    {
        GetData();
    }
    private void GetData()
    {
        _turretData = GameDataRepository.Instance.GetFriendlyTurret();

        GameObject baseObject = GameObject.FindGameObjectWithTag(_turretData.FriendlyBase);
        if (baseObject != null)
        {
            _DetectionOrigin = baseObject.transform;
            _origin = _DetectionOrigin.position;
            _direction = _DetectionOrigin.forward;
            _rotation = _DetectionOrigin.rotation;
        }
        else
        {
            Debug.LogWarning("TurretBaseBehavior: Could not find object with tag " + _turretData.FriendlyBase);
        }
    }


    private void Update()
    {
        CheckForEnemies();
    }

    private void CheckForEnemies()
    {
        if (Physics.BoxCast(_origin, _turretData.BoxSize, _direction, out var hitInfo,
            _DetectionOrigin.rotation, _turretData.Range, _turretData.OppositeUnitLayer))
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
        _currentCoroutine = StartCoroutine(AttackLoop(_turretData.InitialAttackDelay));
    }

    IEnumerator AttackLoop(float insialAttackDelay)
    {
        yield return new WaitForSeconds(insialAttackDelay);
        Instantiate(_turretData.BulletPrefab, _bulletSpawnPos.position, _bulletSpawnPos.rotation);
        _isAttacking = false;
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