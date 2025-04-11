using System.Collections;
using UnityEngine;

public class PopupUI : BaseUI
{
    private CanvasGroup canvasGroup;

    [SerializeField] private float duration = 0.5f;
    private float elapsed = 0f;

    private void OnEnable()
    {
        gameObject.AddComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private IEnumerator FadeOut()
    {
        elapsed = duration;
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        while (elapsed > 0)
        {
            elapsed -= Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            Debug.Log("어두워 지는 중");
            yield return null;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
