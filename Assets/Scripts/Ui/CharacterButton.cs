using Assets.Scripts;
using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    public GameObject _characterPrefab;
    public int _cost;
    public float _deployDelayTime;

    public void OnButtonPress()
    {
        GameManager.Instance.CharacterButtonPressed(this);
    }

    public bool CanDeploy()
    {
        return PlayerCurrency.Instance.Money >= _cost;
    }
}
