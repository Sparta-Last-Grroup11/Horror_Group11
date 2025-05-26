using System;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PrologueUI : PopupUI
{
    private string firstSentence = "\"할아버지께서 돌아가셨다고요..?\"";
    private string secondSentence = "오래간 끊겼던 연락...\n그리고 내 앞으로 난데 없는 세금 고지서가 날라왔다...";
    private string thirdSentence = "땅과 건물에 관한 고지서였고,\n할아버지께서 나에게 남긴 유산이었다...";
    private string fourthSentence = "상속된 것은 작은 시골 마을 외곽에 위치한 오래된 폐병원...";
    private string fifthSentence = "한 때 유명한 정신병원이었지만,\n수십 년 전 알 수 없는 이유로 화재와 함께 운영이 중단되었다고 한다...";
    private string sixthSentence = "나는 건물 내부를 확인하기 위해 병원 안으로 들어갔지만,\n음산한 기운과 함께, 나 혼자만 있는 게 아닌 것 같은 느낌이 든다.....";

    [SerializeField] AudioClip typingClip;
    [SerializeField] AudioClip prologueBGM;
    [SerializeField] private TMP_Text targetText;

    private float delay = 0.1f;
    private float pauseBetweenSentences = 1f;

    private CancellationTokenSource cts;

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

        await TypeSentenceAsync(firstSentence, token);
        AudioManager.Instance.Audio2DPlay(typingClip, 1, false, EAudioType.SFX);
        await Task.Delay((int)(pauseBetweenSentences * 1000), token);

        await TypeSentenceAsync(secondSentence, token);
        AudioManager.Instance.Audio2DPlay(typingClip, 1, false, EAudioType.SFX);
        await Task.Delay((int)(pauseBetweenSentences * 1000), token);

        await TypeSentenceAsync(thirdSentence, token);
        AudioManager.Instance.Audio2DPlay(typingClip, 1, false, EAudioType.SFX);
        await Task.Delay((int)(pauseBetweenSentences * 1000), token);

        await TypeSentenceAsync(fourthSentence, token);
        AudioManager.Instance.Audio2DPlay(typingClip, 1, false, EAudioType.SFX);
        await Task.Delay((int)(pauseBetweenSentences * 1000), token);

        await TypeSentenceAsync(fifthSentence, token);
        AudioManager.Instance.Audio2DPlay(typingClip, 1, false, EAudioType.SFX);
        await Task.Delay((int)(pauseBetweenSentences * 1000), token);

        await TypeSentenceAsync(sixthSentence, token);
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
