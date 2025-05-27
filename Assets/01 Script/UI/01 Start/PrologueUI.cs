using System;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Newtonsoft.Json;
using System.Collections.Generic;

public class PrologueUI : PopupUI
{
    [SerializeField] AudioClip typingClip;
    [SerializeField] AudioClip prologueBGM;
    [SerializeField] private TMP_Text targetText;
    private TextAsset prologueAsset;
    private List<MonologueInfo> dialogList;

    private float delay = 0.1f;
    private float pauseBetweenSentences = 1f;

    private CancellationTokenSource cts;

    private void Awake()
    {
        prologueAsset = ResourceManager.Instance.Load<TextAsset>(ResourceType.JsonData, "Prologue");
        var path = "Prologue";
        var Dialog = JsonConvert.DeserializeObject<Dictionary<string, List<MonologueInfo>>>(prologueAsset.text);
        if (Dialog.TryGetValue(path, out var monologues))
        {
            dialogList = monologues;
        }
    }

    async void Start()
    {
        cts = new CancellationTokenSource();

        // SkipUI 표시 및 스킵 완료 시 토큰 취소 설정
        UIManager.Instance.show<SkipUI>().Init(1f, () =>
        {
            cts.Cancel();
        });

        try
        {
            await PrintAllSentencesAsync(cts.Token);
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Skipped prologue.");
        }

        await SceneLoadManager.Instance.ChangeScene("LobbyScene");
    }

    async Task PrintAllSentencesAsync(CancellationToken token)
    {
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.Audio2DPlay(prologueBGM, 1, false, EAudioType.BGM);
        AudioManager.Instance.Audio2DPlay(typingClip, 1, false, EAudioType.SFX);
        await Task.Delay(1000, token);

        await TypeSentenceAsync(dialogList[0].GetContent(), token);
        AudioManager.Instance.Audio2DPlay(typingClip, 1, false, EAudioType.SFX);
        await Task.Delay((int)(pauseBetweenSentences * 1000), token);

        await TypeSentenceAsync(dialogList[1].GetContent(), token);
        AudioManager.Instance.Audio2DPlay(typingClip, 1, false, EAudioType.SFX);
        await Task.Delay((int)(pauseBetweenSentences * 1000), token);

        await TypeSentenceAsync(dialogList[2].GetContent(), token);
        AudioManager.Instance.Audio2DPlay(typingClip, 1, false, EAudioType.SFX);
        await Task.Delay((int)(pauseBetweenSentences * 1000), token);

        await TypeSentenceAsync(dialogList[3].GetContent(), token);
        AudioManager.Instance.Audio2DPlay(typingClip, 1, false, EAudioType.SFX);
        await Task.Delay((int)(pauseBetweenSentences * 1000), token);

        await TypeSentenceAsync(dialogList[4].GetContent(), token);
        AudioManager.Instance.Audio2DPlay(typingClip, 1, false, EAudioType.SFX);
        await Task.Delay((int)(pauseBetweenSentences * 1000), token);

        await TypeSentenceAsync(dialogList[5].GetContent(), token);
        AudioManager.Instance.Audio2DPlay(typingClip, 1, false, EAudioType.SFX);

        targetText.DOFade(0, 8f);
        await Task.Delay(8000, token);
    }

    async Task TypeSentenceAsync(string sentence, CancellationToken token)
    {
        targetText.text = "";

        foreach (char letter in sentence)
        {
            token.ThrowIfCancellationRequested();
            targetText.text += letter;
            await Task.Delay((int)(delay * 1000), token);
        }
    }

    private void OnDestroy()
    {
        cts?.Cancel();
        cts?.Dispose();
    }
}
