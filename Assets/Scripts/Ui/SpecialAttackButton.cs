using Assets.Scripts;
using Assets.Scripts.Enems;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAttackButton : MonoBehaviour
{
    [SerializeField] private SpecialAttackType _specialAttackType;
    [SerializeField] private GameObject _meteorRainPrefab;
    [SerializeField] private Transform _meteorRainSpawnPos;
    [SerializeField] private int _specialAttackCost;

    public SpecialAttackType Type => _specialAttackType;

    private void Start()
    {
        var sprite = UpgradeStateManager.Instance.GetSpecialAttackSprite(_specialAttackType); 
        if (sprite != null)
            GetComponent<Image>().sprite = sprite;
    }
    public void PerformSpecialAttack()
    {
        if (PlayerCurrency.Instance.HasEnoughMoney(_specialAttackCost) && !MeteorRainAlreadyExists())
        {
            PlayerCurrency.Instance.SubtractMoney(_specialAttackCost);
            ApplySpecialAttack();
        }
    }

    private void ApplySpecialAttack()
    {
        var currentAttack = UpgradeStateManager.Instance.CurrentSpecialAttack;
        var currentAttackPrfab = UpgradeStateManager.Instance.GetSpecialAttackPrefab();
        switch (currentAttack)
        {
            case SpecialAttackType.MeteorRain:
                Instantiate(currentAttackPrfab, _meteorRainSpawnPos.position, _meteorRainSpawnPos.rotation);
                break;
            case SpecialAttackType.SpecialAttack2:
                //same logic for now
                Instantiate(currentAttackPrfab, _meteorRainSpawnPos.position, _meteorRainSpawnPos.rotation);
                break;
            case SpecialAttackType.SpecialAttack3:
                //same logic for now
                Instantiate(currentAttackPrfab, _meteorRainSpawnPos.position, _meteorRainSpawnPos.rotation);
                break;

            default:
                Debug.LogWarning("Unknown SpecialAttackType type: " + _specialAttackType);
                break;
        }
    }

    private bool MeteorRainAlreadyExists()
    {
        return FindAnyObjectByType<MeteorRain>() != null;
    }

}
