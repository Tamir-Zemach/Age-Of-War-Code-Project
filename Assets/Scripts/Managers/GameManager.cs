using Assets.Scripts;
using Assets.Scripts.Data;
using Assets.Scripts.Managers;
using Assets.Scripts.turrets;
using Assets.Scripts.units;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentMonoBehaviour<GameManager>
{
    [Header("Player Starting Stats")]
    [Tooltip("Amount of money the player starts with at the beginning of the game.")]
    [Min(0)]
    [SerializeField] private int _startingMoney;

    [Tooltip("Initial health points for the player.")]
    [Min(1)]
    [SerializeField] private int _startingHealth;

    [Header("Enemy Health Per Level")]
    [Tooltip("Enemy base health for Level 1.")]
    [Min(1)]
    [SerializeField] private int _level1EnemyHealth;

    
    [Tooltip("Enemy base health for Level 2.")]
    [Min(1)]
    [SerializeField] private int _level2EnemyHealth;

    [Tooltip("Enemy base health for Level 3.")]
    [Min(1)]
    [SerializeField] private int _level3EnemyHealth;

    // Internal data used for unit upgrades per age
    private List<UnitData> _friendlyUnitData;
    private List<UnitLevelUpData> _unitLevelUpData;

    private SpecialAttackData _specialAttackData;
    private SpecialAttackLevelUpData _specialAttackLevelUpData;

    private TurretData _turretData;
    private TurretLevelUpData _turretLevelUpData;

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
        EnemyHealth.Instance.SetMaxHealth(SetEnemyBaseHealthForCurrentLevel(LevelLoader.Instance.LevelIndex));
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

    public void UpgradePlayerAge()
    {
        _friendlyUnitData = GameDataRepository.Instance.GetAllFriendlyUnits();
        _unitLevelUpData = GameDataRepository.Instance.GetUnitLevelUpData();
        _specialAttackData = GameDataRepository.Instance.GetSpecialAttack();
        _specialAttackLevelUpData = GameDataRepository.Instance.GetSpecialAttackLevelUpData();

        _turretData = GameDataRepository.Instance.GetFriendlyTurret();
        _turretLevelUpData = GameDataRepository.Instance.GetTurretLevelUpData();

        AgeUpgrade.Instance.AdvanceAge(isFriendly: true);
        AgeUpgrade.Instance.ApplyUpgradesToUnits(
            _friendlyUnitData,
            _unitLevelUpData,
            AgeUpgrade.Instance.CurrentPlayerAge,
            isFriendly: true
        );
        AgeUpgrade.Instance.ApplySpecialAttackUpgrade(_specialAttackData, _specialAttackLevelUpData, AgeUpgrade.Instance.CurrentPlayerAge, isFriendly: true);
        AgeUpgrade.Instance.ApplyTurretUpgrade(_turretData, _turretLevelUpData, AgeUpgrade.Instance.CurrentPlayerAge, isFriendly: true);
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
    }
}