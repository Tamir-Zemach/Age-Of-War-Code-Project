using Assets.Scripts;
using TMPro;
using UnityEngine;

public class CurrencyUiDisplay : MonoBehaviour
{
    private TextMeshProUGUI moneyText;
    private void Awake()
    {
        moneyText = GetComponent<TextMeshProUGUI>();
        UpdateMoneyUI();
        PlayerCurrency.OnMoneyChanged += UpdateMoneyUI; 
    }
    public void UpdateMoneyUI()
    {
        moneyText.text = $"Money: {PlayerCurrency.Instance.Money}";
    }
}
