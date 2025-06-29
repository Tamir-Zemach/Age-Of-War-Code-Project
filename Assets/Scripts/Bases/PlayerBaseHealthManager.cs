using Assets.Scripts.Managers;
using UnityEngine;
using Assets.Scripts.InterFaces;
public class PlayerBaseHealthManager : MonoBehaviour, IDamageable
{

    public void GetHurt(int damage)
    {
        PlayerHealth.Instance.SubtractHealth(damage);
    }
}
