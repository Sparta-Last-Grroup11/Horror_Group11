using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class QuestUI : BaseUI
{
    private CanvasGroup canvasGroup;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] AudioClip questNotifySound;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float typingSpeed = 0.05f;
    private float elapsed = 0f;

    protected override void Start()
    {
        base.Start();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        QuestManager.Instance.GetQuestUI(this);
        text = GetComponentInChildren<TextMeshProUGUI>(true);
    }
    public void ChangeQuest(string input = null)
    {
        StartCoroutine(PlayFadeSequence(input));
    }

    private IEnumerator PlayFadeSequence(string input)
    {
        yield return null;
        if (canvasGroup.alpha > 0)
            yield return StartCoroutine(FadeOut());

        // Ensure canvas is visible before typing
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        text.text = ""; // Clear text before typing
        AudioManager.Instance.Audio2DPlay(questNotifySound, 1f, false, EAudioType.SFX);

        yield return StartCoroutine(TypewriterEffect(input));
    }

    private IEnumerator TypewriterEffect(string fullText)
    {
        text.text = ""; // Start with empty text
        foreach (char c in fullText)
        {
            text.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    //private IEnumerator PlayFadeSequence(string input)
    //{
    //    yield return null;
    //    if(canvasGroup.alpha > 0)
    //        yield return StartCoroutine(FadeOut());

    //    text.text = input;
    //    AudioManager.Instance.Audio2DPlay(questNotifySound, 1f, false, EAudioType.SFX);

    //    yield return StartCoroutine(FadeIn());
    //}

    //private IEnumerator FadeIn()
    //{
    //    canvasGroup.alpha = 0f;
    //    canvasGroup.interactable = false;
    //    canvasGroup.blocksRaycasts = false;

    //    while (elapsed < duration)
    //    {
    //        elapsed += Time.deltaTime;
    //        canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
    //        yield return null;
    //    }

    //    canvasGroup.alpha = 1f;
    //    canvasGroup.interactable = true;
    //    canvasGroup.blocksRaycasts = true;
    //}

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
            yield return null;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
