using Assets.Scripts;
using UnityEngine;

public class Admin : MonoBehaviour
{
    public int _moneyToAdd;
    public int _moneyToSubstract;
    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerCurrency.Instance.AddMoney(_moneyToAdd);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerCurrency.Instance.SubtractMoney(_moneyToSubstract);
        }
    }
    private void Update()
    {
        Test();
    }

}
