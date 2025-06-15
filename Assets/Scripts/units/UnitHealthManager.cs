using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(UnitBaseBehaviour))]
public class UnitHealthManager : MonoBehaviour
{
    private Unit Unit;
    private UnitBaseBehaviour UnitBaseBehaviour;
    private int _currentHealth;
    private void Awake()
    {
        UnitBaseBehaviour = GetComponent<UnitBaseBehaviour>();
        Unit = UnitBaseBehaviour.Unit;
        _currentHealth = Unit._health;
    }

        public void GetHurt(int damage)
        {
            _currentHealth -= damage;
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


