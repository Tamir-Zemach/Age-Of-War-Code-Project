using UnityEngine;

public class TurretSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject highlightGfx;

    public bool HasTurret;
    public bool IsUnlocked { get; set; }

    public void ShowHighlight(bool show)
    {
        if (highlightGfx != null)
            highlightGfx.SetActive(show);
    }
}
