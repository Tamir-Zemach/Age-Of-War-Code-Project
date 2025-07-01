using Assets.Scripts;
using Assets.Scripts.Managers;
using UnityEngine;

public class GameManager : PersistentMonoBehaviour<GameManager>
{
    [SerializeField] private int _startingMoney;
    [SerializeField] private int _startingHealth;

    [SerializeField] private int _level1EnemyHealth;
    [SerializeField] private int _level2EnemyHealth;
    [SerializeField] private int _level3EnemyHealth;


    protected override void Awake()
    {
        base.Awake();
        EnemyHealth.Instance.OnEnemyDied += NextLevel;
        StartGame();
    }

    private void Update()
    {
        if (PlayerHealth.Instance.PlayerDied())
        {
            GameOver();
        }
    }

    private void StartGame()
    {
        PlayerCurrency.Instance.AddMoney(_startingMoney);
        PlayerHealth.Instance.SetMaxHealth(_startingHealth);
        PlayerHealth.Instance.FullHealth();
    }

    public void NextLevel()
    {
        LevelLoader.Instance.LoadNextLevel();
        EnemyHealth.Instance.SetMaxHealth
            (SetEnemyBaseHealthForCurrentLevel(LevelLoader.Instance.LevelIndex));
        EnemyHealth.Instance.FullHealth();  
    }



    private void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
    }


    public int SetEnemyBaseHealthForCurrentLevel(int levelIndex)
    {
        switch (levelIndex)
        {
            case 1: return _level1EnemyHealth;
            case 2: return _level2EnemyHealth;
            case 3: return _level3EnemyHealth;
            default:
                Debug.LogWarning("Invalid level. Defaulting to Level 1 health.");
                return _level1EnemyHealth;
        }
    }


}