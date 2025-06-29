using Assets.Scripts;
using Assets.Scripts.Data;
using Assets.Scripts.Enems;
using UnityEngine;

public class SpecialAttackButton : MonoBehaviour
{
    [SerializeField] private SpecialAttackType _specialAttackType;
    [SerializeField] private GameObject _meteorRainPrefab;
    [SerializeField] private Transform _meteorRainSpawnPos;
    [SerializeField] private int _specialAttackCost;





    private void Start()
    {
        var sprite = UpgradeStateManager.Instance.GetSpecialAttackSprite(); // if you store it
        if (sprite != null)
            GetComponent<UnityEngine.UI.Image>().sprite = sprite;
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
        switch (currentAttack)
        {
            case SpecialAttackType.MeteorRain:
                Instantiate(_meteorRainPrefab, _meteorRainSpawnPos.position, _meteorRainSpawnPos.rotation);
                break;
            case SpecialAttackType.SpecialAttack2:
                //same logic for now
                Instantiate(_meteorRainPrefab, _meteorRainSpawnPos.position, _meteorRainSpawnPos.rotation);
                break;
            case SpecialAttackType.SpecialAttack3:
                //same logic for now
                Instantiate(_meteorRainPrefab, _meteorRainSpawnPos.position, _meteorRainSpawnPos.rotation);
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
