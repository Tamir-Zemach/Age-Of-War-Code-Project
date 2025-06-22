using System;
using UnityEngine;
using Assets.Scripts.units.Behavior;
public class EnemyBaseHealthManger : MonoBehaviour, IDamageable
{
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
            Debug.Log("next level");
            Time.timeScale = 0;
        }
    }
}
