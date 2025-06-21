using System.Collections;
using UnityEngine;

public class TurretSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject highlightGfx;

    public bool HasTurret;
    public bool IsUnlocked { get; set; }

    private Coroutine _flashRoutine;
    public void ShowHighlight(bool show)
    {
        if (highlightGfx != null)
            highlightGfx.SetActive(show);
    }

    public void StartFlashing(float timeBetweenFlashes)
    {
        if (_flashRoutine == null)
            _flashRoutine = StartCoroutine(FlashLoop(timeBetweenFlashes));
    }

    public void StopFlashing()
    {
        if (_flashRoutine != null)
        {
            StopCoroutine(_flashRoutine);
            highlightGfx.SetActive(false);
            _flashRoutine = null;
        }
    }

    private IEnumerator FlashLoop(float timeBetweenFlashes)
    {
        while (true)
        {
            highlightGfx.SetActive(true);
            yield return new WaitForSeconds(timeBetweenFlashes);
            highlightGfx.SetActive(false);
            yield return new WaitForSeconds(timeBetweenFlashes);
        }
    }
}
