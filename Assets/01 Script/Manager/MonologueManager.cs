using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologueManager : Singleton<MonologueManager>
{
    Queue<string> dialogQueue;
    GameObject dialogPrefab;
    public bool isPlaying = false;
    protected override bool dontDestroy => false;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        dialogQueue = new Queue<string>();
    }

    public void DialogPlay(string input)
    {
        dialogQueue.Enqueue(input);
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
