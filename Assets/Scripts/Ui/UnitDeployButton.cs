using Assets.Scripts;
using Assets.Scripts.Enems;
using UnityEngine;
using UnityEngine.UI;

public class UnitDeployButton : MonoBehaviour
{
    [SerializeField] private UnitType _unitType;

    private UnitData unit;

    public UnitType UnitType => _unitType;


    private void Start()
    {
        unit = GameManager.Instance.GetInstantiatedUnit(_unitType);

        Sprite finalSprite = UpgradeStateManager.Instance.GetUnitSprite(_unitType) ?? unit._spriteForUi;
        GetComponent<Image>().sprite = finalSprite;
    }
    public void DeployUnit()
    {
        if (PlayerCurrency.Instance.HasEnoughMoney(unit._cost))
        {
            PlayerCurrency.Instance.SubtractMoney(unit._cost);

            if (GameManager.Instance != null && DeployManager.Instance != null)
            {
                DeployManager.Instance.AddUnitToDeploymentQueue(unit);
            }
            else
            {
                Debug.LogWarning("DeployUnit called before GameManager or DeployManager were initialized.");
            }
        }


    }
}
