using UnityEngine;

public class EnemyBaseHealthManger : MonoBehaviour
{
    private int _currentHealth;
    private void Start()
    {
        _currentHealth = GameManager.Instance.Level1EnemyBaseHealth;
    }


    public void GetHurt(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Debug.Log("next level");
            Time.timeScale = 0;
        }
    }
}
