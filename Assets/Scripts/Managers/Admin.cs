using Assets.Scripts;
using Assets.Scripts.Managers;
using UnityEngine;

public class Admin : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    EnemySpawner EnemySpawner;

    public int _moneyToAdd;
    public int _moneyToSubstract;
    public int _healthToAdd;

    public int _gameSpeed = 1 ;


    public Unit[] unitToDisplayParameters;
    public bool _displayFrienlyUnitParameters;
    public bool _displayEnemyUnitParameters;

    public bool _easyMode;
    [SerializeField] private float _easyModeMinSpawnTime;
    [SerializeField] private float _easyModeMaxSpawnTime;
    private void AdminFunctions()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerCurrency.Instance.AddMoney(_moneyToAdd);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerCurrency.Instance.SubtractMoney(_moneyToSubstract);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerHealth.Instance.AddHealth(_healthToAdd);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameSpeedControl();
        }
        if (_easyMode)
        {
            EasyMode();
        }
        if (_displayFrienlyUnitParameters)
        {
            DisplayFriendlyUnitParametersFromGameManager();
        }
        if (_displayEnemyUnitParameters)
        {
            DisplayEnemyUnitParametersFromGameManager();
        }
    }

    private void GameSpeedControl() 
    {
        Time.timeScale = _gameSpeed;
    }
    private void EasyMode()
    {
        EnemySpawner = gameManager.GetComponent<EnemySpawner>();
        EnemySpawner.EasyMode(_easyModeMinSpawnTime, _easyModeMaxSpawnTime);
    }

    public void DisplayFriendlyUnitParametersFromGameManager()
    {
        foreach (var kvp in GameManager.ModifiedUnitData)
        {
            Unit un = kvp.Value;
            Debug.Log($"{un.name} " +
                      $"Unit Cost: {un._cost}" +
                      $"speed: {un._speed}, " +
                      $"Strength: {un._strength}, " +
                      $"Attack Speed (Initial Attack Delay): {un._initialAttackDelay}, " +
                      $"Range: {un._range}");
        }
    }
    public void DisplayEnemyUnitParametersFromGameManager()
    {
        foreach (var kvp in EnemySpawner._enemyUnitData)
        {
            Unit un = kvp.Value;
            Debug.Log($"{un.name} " +
                      $"Unit Money Gain: {un._moneyWhenKilled}" +
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
