using System;
using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(UnitBaseBehaviour))]
public class UnitHealthManager : MonoBehaviour
{
    public event Action OnHealthChanged;
    private Unit Unit;
    private UnitBaseBehaviour UnitBaseBehaviour;
    private int _currentHealth;
    private void Start()
    {
        UnitBaseBehaviour = GetComponent<UnitBaseBehaviour>();
        Unit = UnitBaseBehaviour.Unit;
        _currentHealth = Unit._health;
    }

    public void GetHurt(int damage)
        {
        print($"{gameObject.name} is getting hurt");
            _currentHealth -= damage;
            OnHealthChanged?.Invoke();
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
        PlayerCurrency.Instance.AddMoney(Unit._moneyWhenKilled);
        //TODO: Death Animation
        Destroy(gameObject); 
        }
    }


