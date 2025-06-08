using Assets.Scripts;
using TMPro;
using UnityEngine;

public class CurrencyUiDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    private void Awake()
    {
        UpdateMoneyUI();
        PlayerCurrency.OnMoneyChanged += UpdateMoneyUI;
    }


    public void UpdateMoneyUI()
    {
        moneyText.text = $"Money: {PlayerCurrency.Instance.Money}";
    }

}

