using System.Collections;
using UnityEngine;

public class FadePanelUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    [SerializeField] private float fadeDuration = 0.5f;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;

        gameObject.SetActive(false);
    }

    public void FadeOutCanvas()
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeOut());
    }

    public void FadeInCanvas()
    { 
    StartCoroutine (FadeIn());
    }

    public IEnumerator FadeOut()
    {
        canvasGroup.blocksRaycasts = true;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    public IEnumerator FadeIn()
    {
        Debug.Log("FadeIn 진입2");
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = 1f - Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;

        gameObject.SetActive(false);

    }
}
