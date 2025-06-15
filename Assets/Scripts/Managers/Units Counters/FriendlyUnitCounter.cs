using UnityEngine;

public class FriendlyUnitCounter : MonoBehaviour
{
    public static int UnitCount { get; private set; }
    private void Awake()
    {
        UnitCount++;
    }

    private void OnDestroy()
    {
        UnitCount--;
    }
}

