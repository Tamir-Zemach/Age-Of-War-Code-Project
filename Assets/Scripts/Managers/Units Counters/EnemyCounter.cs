using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public static int EnemyCount { get; private set; } 
    private void Awake()
    {
        EnemyCount++;
    }

    private void OnDestroy()
    {
        EnemyCount--;
    }
}