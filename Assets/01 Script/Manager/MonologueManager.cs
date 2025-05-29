using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public enum LanguageType
{
    English,
    Korean
}
[System.Serializable]
public class MonologueInfo
{
    public int number;
    public string en;
    public string kr;

    public string GetContent()
    {
        switch(LocalizationSettings.SelectedLocale.Identifier.Code)
        {
            case "en":
                return en;
            case "kr":
                return kr;
            default:
                return en;
        }
    }
}

public struct MonologueWithAudio
{
    public string content;
    public AudioClip clip;

    public MonologueWithAudio(string str, AudioClip audio = null)
    {
        content = str;
        clip = audio;
    }
}


//대화 매니저. 큐의 선입선출을 이용해서 대기열을 만들어 차근차근 대화를 진행시킴.
//강제로 현재 대화를 종료하고 다음 순서를 진행시키는 메서드도 구현 가능.
public class MonologueManager : Singleton<MonologueManager>
{
    Queue<MonologueWithAudio> monologueQueue;
    [SerializeField] private TextAsset monologueAsset;
    [SerializeField] List<MonologueInfo> dialogList;
    string langType;

    GameObject dialogPrefab;
    public bool isPlaying = false;
    protected override bool dontDestroy => false;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        langType = LocalizationSettings.SelectedLocale.Identifier.Code;
        monologueAsset = ResourceManager.Instance.Load<TextAsset>(ResourceType.JsonData, "Monologue");
        monologueQueue = new Queue<MonologueWithAudio>();
        var path = "Monologue";
        var Dialog = JsonConvert.DeserializeObject<Dictionary<string, List<MonologueInfo>>>(monologueAsset.text);

        if (Dialog.TryGetValue(path, out var monologues))
        {
            dialogList = monologues;
        }
    }

    public void DialogPlay(string input)
    {
        monologueQueue.Enqueue(new MonologueWithAudio(input));
    }

    //해당 번호에 해당하는 텍스트를 Json에서 읽어온 Id와 대조해서 넣고. 리소스 폴더에 id에 해당하는 파일이 있는 지 확인 후 대사 음성을 같이 정보에 넣음(없으면 대사 음성 출력 안함)
    public void DialogPlay(int number)
    {
        if (dialogList.Count >= number)
        {
            AudioClip clip = ResourceManager.Instance.Load<AudioClip>(ResourceType.Sound, $"2D/TTS/{number}");
            MonologueWithAudio mono = new MonologueWithAudio(dialogList[number].GetContent(), clip);
            if (monologueQueue.Contains(mono))
            {
                return;
            }
            monologueQueue.Enqueue(mono);
        }
    }

    private void Update()
    {
        if (monologueQueue.Count > 0 && isPlaying == false)
            PlayQueue();
    }

    private void ToggleLocale()
    {
        var currentLocale = LocalizationSettings.SelectedLocale.Identifier.Code;

        // 영어(en) → 한국어(ko), 한국어(ko) → 영어(en)
        string nextLocaleCode = currentLocale == "en" ? "kr" : "en";

        Locale nextLocale = null;
        foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
        {
            if (locale.Identifier.Code == nextLocaleCode)
            {
                nextLocale = locale;
                break;
            }
        }

        if (nextLocale != null)
        {
            LocalizationSettings.SelectedLocale = nextLocale;
            Debug.Log("Switched locale to: " + nextLocale.Identifier.Code);
        }
        else
        {
            Debug.LogWarning("Target locale not found: " + nextLocaleCode);
        }
    }

    private void PlayQueue()
    {
        var obj = monologueQueue.Dequeue();
        UIManager.Instance.show<DialogUI>().Init(obj.content);
        if (obj.clip != null)
            AudioManager.Instance.Audio2DPlay(obj.clip, 1, false, EAudioType.SFX);
    }
}
