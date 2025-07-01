using Assets.Scripts;
using Assets.Scripts.Managers;
using UnityEngine;

public class Admin : PersistentMonoBehaviour<Admin>
{

    EnemySpawner EnemySpawner;

    public int _moneyToAdd;
    public int _moneyToSubstract;
    public int _healthToAdd;

    public int _gameSpeed = 1 ;

    public bool _displayFrienlyUnitParametersInConsole;
    public bool _displayEnemyUnitParametersInConsole;
    public bool _displayHealthInConsole;

    //[SerializeField] GameManager gameManager;
    //public bool _easyMode;

    [SerializeField] private float _easyModeMinSpawnTime;
    [SerializeField] private float _easyModeMaxSpawnTime;


    private void AdminFunctions()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayerCurrency.Instance.AddMoney(_moneyToAdd);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayerCurrency.Instance.SubtractMoney(_moneyToSubstract);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerHealth.Instance.AddHealth(_healthToAdd);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameSpeedControl();
        }
        //if (_easyMode)
        //{
        //    EasyMode();
        //}
        if (_displayFrienlyUnitParametersInConsole)
        {
            DisplayFriendlyUnitParameters();
        }
        if (_displayEnemyUnitParametersInConsole)
        {
            DisplayEnemyUnitParameters();
        }
        if (_displayHealthInConsole)
        {
            PlayerHealth.Instance.DisplyHealthInConsole();
        }
    }

    private void GameSpeedControl() 
    {
        Time.timeScale = _gameSpeed;
    }
    //private void EasyMode()
    //{
    //    EnemySpawner = gameManager.GetComponent<EnemySpawner>();
    //    EnemySpawner.EasyMode(_easyModeMinSpawnTime, _easyModeMaxSpawnTime);
    //}

    public void DisplayFriendlyUnitParameters()
    {
        foreach (var unit in GameDataRepository.Instance.GetAllFriendlyUnits())
        {
            Debug.Log($"{unit.name} " +
                      $"Unit Cost: {unit._cost}, " +
                      $"Health: {unit._health}, " +
                      $"Speed: {unit._speed}, " +
                      $"Strength: {unit._strength}, " +
                      $"Attack Speed (Initial Attack Delay): {unit._initialAttackDelay}, " +
                      $"Range: {unit._range}");
        }
    }
    public void DisplayEnemyUnitParameters()
    {
        foreach (var unit in GameDataRepository.Instance.GetAllEnemyUnits())
        {
            Debug.Log($"{unit.name} " +
                      $"Reward: {unit._moneyWhenKilled}, " +
                      $"Health: {unit._health}, " +
                      $"Speed: {unit._speed}, " +
                      $"Strength: {unit._strength}, " +
                      $"Attack Speed (Initial Attack Delay): {unit._initialAttackDelay}, " +
                      $"Range: {unit._range}");
        }
    }

    private void Update()
    {
        AdminFunctions();
    }

}
