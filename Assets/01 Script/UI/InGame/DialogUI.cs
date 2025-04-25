using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogUI : BaseUI
{
    Queue<string> dialogQueue;
    [SerializeField] TextMeshProUGUI text;
    bool isPlaying = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        dialogQueue = new Queue<string>();
        text = GetComponentInChildren<TextMeshProUGUI>(true);
        text.enabled = false;
    }

    public void DialogPlay(string input)
    {
        dialogQueue.Enqueue(input);
    }

    private void Update()
    {
        if (dialogQueue.Count > 0 && isPlaying == false)
            StartCoroutine(DialogAction());
    }

    IEnumerator DialogAction()
    {
        isPlaying = true;
        text.text = dialogQueue.Dequeue();
        text.enabled = true;
        yield return new WaitForSeconds(3);
        text.enabled = false;
        isPlaying = false;
    }
}
