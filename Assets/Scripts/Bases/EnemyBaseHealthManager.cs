using System;
using UnityEngine;
using Assets.Scripts.units.Behavior;
using UnityEngine.Events;
public class EnemyBaseHealthManger : MonoBehaviour, IDamageable
{
    public UnityEvent _onEnemyDefeat;

    public event Action OnEnemyHealthChanged;
    private int _currentHealth;
    private int _maxHealth;
    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    private void Start()
    {
        _maxHealth = GameManager.Instance.Level1EnemyBaseHealth;
        _currentHealth = _maxHealth;
    }


    public void GetHurt(int damage)
    {
        _currentHealth -= damage;
        OnEnemyHealthChanged?.Invoke();
        if (_currentHealth <= 0)
        {
            _onEnemyDefeat?.Invoke();
        }
    }
}
