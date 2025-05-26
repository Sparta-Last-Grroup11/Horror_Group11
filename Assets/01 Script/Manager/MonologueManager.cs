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
