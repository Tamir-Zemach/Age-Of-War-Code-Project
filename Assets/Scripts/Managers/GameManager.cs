using Assets.Scripts;
using Assets.Scripts.Managers;
using Assets.Scripts.units;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentMonoBehaviour<GameManager>
{
    [SerializeField] private int _startingMoney;
    [SerializeField] private int _startingHealth;

    [SerializeField] private int _level1EnemyHealth;
    [SerializeField] private int _level2EnemyHealth;
    [SerializeField] private int _level3EnemyHealth;

    private List<UnitData> _friendlyUnitData;
    private List<UnitLevelUpData> _unitLevelUpData;


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
        if (LevelLoader.Instance.LevelIndex != 0)
        {
            UpgradePlayerAge();
        }
        LevelLoader.Instance.LoadNextLevel();
        ResetEnemyHealth();
    }

    public void ResetEnemyHealth()
    {
        EnemyHealth.Instance.SetMaxHealth
        (SetEnemyBaseHealthForCurrentLevel(LevelLoader.Instance.LevelIndex));
        EnemyHealth.Instance.FullHealth();
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

    private void UpgradePlayerAge()
    {
        _friendlyUnitData = GameDataRepository.Instance.GetAllFriendlyUnits();
        _unitLevelUpData = GameDataRepository.Instance.GetUnitLevelUpData();
        AgeUpgrade.Instance.AdvanceAge(isFriendly: true);
        AgeUpgrade.Instance.ApplyUpgradesToUnits(_friendlyUnitData, _unitLevelUpData, AgeUpgrade.Instance.CurrentPlayerAge, isFriendly: true);
        AgeUpgrade.Instance.ApplySpecialAttackUpgrade();
        AgeUpgrade.Instance.ApplyTurretUpgrade();
    }


    private void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
    }

}