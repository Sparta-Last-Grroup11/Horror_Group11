using System.Collections;
using UnityEngine;

public abstract class PopupUI : BaseUI
{
    private CanvasGroup canvasGroup;

    [SerializeField] private float duration = 0.5f;
    private float elapsed = 0f;

    [Header("Destroy About")]
    [SerializeField] private bool DestroyByTime;
    [SerializeField] private float DestroyTime;

    protected override void Start()
    {
        base.Start();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

            StartCoroutine(FadeIn());

        if (!DestroyByTime)
            return;

        Invoke(nameof(FadeOutAndDestroyFunc), DestroyTime + duration);
    }

    public virtual void Init(string input = null)
    {
    }

    public IEnumerator FadeIn()
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

    public IEnumerator FadeOut()
    {
        elapsed = duration;
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        while (elapsed > 0)
        {
            elapsed -= Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void FadeOutAndDestroyFunc()
    {
        StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        yield return StartCoroutine(FadeOut());
        Destroy(gameObject);
    }
}
