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
    [SerializeField, TagSelector] private string _enemyBaseTag;
    [SerializeField] private float _speed; 
    [SerializeField] private float _cost; 
    [SerializeField] private float _health;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyBase = GameObject.FindGameObjectWithTag(_enemyBaseTag);
        _agent.destination = _enemyBase.transform.position;
    }
}
