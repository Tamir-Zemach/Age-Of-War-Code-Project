using Assets.Scripts;
using UnityEngine;

public class Admin : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    EnemySpawner EnemySpawner;
    public int _moneyToAdd;
    public int _moneyToSubstract;
    public int _gameSpeed = 1 ;
    public bool _easyMode;
    [SerializeField] private float _easyModeMinSpawnTime;
    [SerializeField] private float _easyModeMaxSpawnTime;
    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayerCurrency.Instance.AddMoney(_moneyToAdd);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerCurrency.Instance.SubtractMoney(_moneyToSubstract);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameSpeedControl();
        }
        if (_easyMode)
        {
            EasyMode();
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

    private void Update()
    {
        Test();
    }

}
