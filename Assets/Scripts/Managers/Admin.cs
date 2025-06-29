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
            DisplayFriendlyUnitParametersFromGameManager();
        }
        if (_displayEnemyUnitParametersInConsole)
        {
            DisplayEnemyUnitParametersFromGameManager();
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

    public void DisplayFriendlyUnitParametersFromGameManager()
    {
        foreach (var kvp in GameManager.ModifiedFriendlyUnitData)
        {
            UnitData un = kvp.Value;
            Debug.Log($"{un.name} " +
                      $"Unit Cost: {un._cost}," +
                      $"Health: {un._health} " +
                      $"Speed: {un._speed}, " +
                      $"Strength: {un._strength}, " +
                      $"Attack Speed (Initial Attack Delay): {un._initialAttackDelay}, " +
                      $"Range: {un._range}");
        }
    }
    public void DisplayEnemyUnitParametersFromGameManager()
    {
        foreach (var kvp in GameManager.ModifiedEnemyUnitData)
        {
            UnitData un = kvp.Value;
            Debug.Log($"{un.name} " +
                      $"Unit Money Gain: {un._moneyWhenKilled}" +
                      $"Health: {un._health} " +
                      $"speed: {un._speed}, " +
                      $"Strength: {un._strength}, " +
                      $"Attack Speed (Initial Attack Delay): {un._initialAttackDelay}, " +
                      $"Range: {un._range}");
        }
    }

    private void Update()
    {
        AdminFunctions();
    }

}
