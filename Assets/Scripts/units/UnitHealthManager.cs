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
            Debug.Log($"Took {damage} damage! Current health: {_currentHealth}");

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("Character has died!");
            gameObject.SetActive(false); // Or trigger death animation
        }
    }


