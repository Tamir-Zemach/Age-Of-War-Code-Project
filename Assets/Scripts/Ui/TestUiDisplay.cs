using Assets.Scripts;
using Assets.Scripts.Managers;
using TMPro;
using UnityEngine;

public class TestUiDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _healthText;

    [SerializeField] private GameObject _currentEnemyBase;
    [SerializeField] private TextMeshProUGUI _enemyhealthText;

    private EnemyBaseHealthManger _enemyHealthManger;

    private void Awake()
    {
        _enemyHealthManger = _currentEnemyBase.GetComponent<EnemyBaseHealthManger>();
        UpdateMoneyUI();
        UpdateHealthUI();
        PlayerCurrency.OnMoneyChanged += UpdateMoneyUI;
        PlayerHealth.OnHealthChanged += UpdateHealthUI;
    }

    private void Start()
    {
        UpdateEnemyHealthUI();
        _enemyHealthManger.OnEnemyHealthChanged += UpdateEnemyHealthUI;
    }


    public void UpdateMoneyUI()
    {
        if (_moneyText == null)
        {
            return;
        }
        _moneyText.text = $"Money: {PlayerCurrency.Instance.Money}";
    }

    public void UpdateHealthUI()
    {
        if (_healthText == null)
        {
            return;
        }
        _healthText.text = $"Current health: {PlayerHealth.Instance.CurrentHealth}, Max Health: {PlayerHealth.Instance.MaxHealth}";
    }

    public void UpdateEnemyHealthUI()
    {
        if (_enemyhealthText == null)
        {
            return;
        }
        _enemyhealthText.text = $"Enemy Current health: {_enemyHealthManger.CurrentHealth}, Enemy Max Health: {_enemyHealthManger.MaxHealth}";
    }



}

