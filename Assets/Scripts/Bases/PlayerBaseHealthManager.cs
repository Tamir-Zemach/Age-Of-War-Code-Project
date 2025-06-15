using Assets.Scripts.Managers;
using UnityEngine;

public class PlayerBaseHealthManager : MonoBehaviour
{

    public void GetHurt(int damage)
    {
        PlayerHealth.Instance.SubtractHealth(damage);
    }
}
