using Assets.Scripts;
using Assets.Scripts.Enems;
using UnityEngine;

public class SpecialAttackButton : MonoBehaviour
{
    [SerializeField] private SpecialAttackType _specialAttackType;
    [SerializeField] private GameObject _meteorRainPrefab;
    [SerializeField] private Transform _meteorRainSpawnPos;
    [SerializeField] private int _specialAttackCost;
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
        switch (_specialAttackType)
        {
            case SpecialAttackType.MeteorRain:
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
