using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonologueInfo
{
    public int number;
    public string content;
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

    GameObject dialogPrefab;
    public bool isPlaying = false;
    protected override bool dontDestroy => false;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
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
            monologueQueue.Enqueue(new MonologueWithAudio(dialogList[number].content, clip));
        }
    }

    private void Update()
    {
        if (monologueQueue.Count > 0 && isPlaying == false)
            PlayQueue();
    }

    private void PlayQueue()
    {
        var obj = monologueQueue.Dequeue();
        UIManager.Instance.show<DialogUI>().Init(obj.content);
        if (obj.clip != null)
            AudioManager.Instance.Audio2DPlay(obj.clip, 1, false, EAudioType.SFX);
    }
}
