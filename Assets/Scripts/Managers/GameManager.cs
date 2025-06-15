
using Assets.Scripts;
using Assets.Scripts.Managers;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private int _startingMoney;
    [SerializeField] private int _startingHealth;
    [SerializeField] private int _level1EnemyBaseHealth; 
    public int Level1EnemyBaseHealth { get; private set; } 

    private void Awake()
    {
        Instance = this; 
        PlayerCurrency.Instance.AddMoney(_startingMoney);
        PlayerHealth.Instance.AddHealth(_startingHealth);

        Level1EnemyBaseHealth = _level1EnemyBaseHealth; 
    }
    private void Update()
    {
        PlayerHealth.Instance.DisplyHealthInConsole();
        if (PlayerHealth.Instance.PlayerDied())
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
    }
}





