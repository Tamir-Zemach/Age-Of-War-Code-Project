using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UiFade : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Coroutine currentFade;
    private bool _isWaitingForClick;
    private bool shouldShow;

    public float fadeDuration = 0.3f;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ToggleFade()
    {
        shouldShow = canvasGroup.alpha <= 0f;
        if (currentFade != null)
            StopCoroutine(currentFade);
        if (shouldShow)
            StartCoroutine(WaitForSlotClick());

        currentFade = StartCoroutine(FadeTo(shouldShow ? 1f : 0f));
        canvasGroup.interactable = shouldShow;
        canvasGroup.blocksRaycasts = shouldShow;
    }

    private IEnumerator WaitForSlotClick()
    {
        _isWaitingForClick = true;
        yield return new WaitForSeconds(0.1f);
        while (_isWaitingForClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var hit = MouseRayCaster.Instance.GetHit();


                if (!hit.HasValue && !EventSystem.current.IsPointerOverGameObject())
                {
                    StartCoroutine(FadeTo(0f));
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                    _isWaitingForClick = false;
                    yield break;
                }
            }

            yield return null;
        }
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }





}
