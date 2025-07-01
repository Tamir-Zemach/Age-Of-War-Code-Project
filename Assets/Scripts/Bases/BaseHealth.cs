using Assets.Scripts.Managers;
using UnityEngine;
using Assets.Scripts.InterFaces;
using System;
public class BaseHealth : MonoBehaviour, IDamageable
{
    public bool isFriendly;


    public void GetHurt(int damage)
    {
        if (isFriendly)
        {
            PlayerHealth.Instance.SubtractHealth(damage);
            return;
        }
        else
        {
            EnemyHealth.Instance.SubtractHealth(damage);
        }

    }

}
