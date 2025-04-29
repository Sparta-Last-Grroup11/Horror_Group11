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

public class MonologueManager : Singleton<MonologueManager>
{
    Queue<string> dialogQueue;
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
        dialogQueue = new Queue<string>();
        var path = "Monologue";
        var Dialog = JsonConvert.DeserializeObject<Dictionary<string, List<MonologueInfo>>>(monologueAsset.text);

        if (Dialog.TryGetValue(path, out var monologues))
        {
            dialogList = monologues;
        }
    }

    public void DialogPlay(string input)
    {
        dialogQueue.Enqueue(input);
    }

    public void DialogPlay(int number)
    {
        if (dialogList.Count >= number)
            dialogQueue.Enqueue(dialogList[number].content);
    }

    private void Update()
    {
        if (dialogQueue.Count > 0 && isPlaying == false)
            PlayQueue();
    }

    private void PlayQueue()
    {
        UIManager.Instance.show<DialogUI>().Init(dialogQueue.Dequeue());
    }
}
