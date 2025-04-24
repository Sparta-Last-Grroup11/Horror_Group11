using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogUI : BaseUI
{
    Queue<string> dialogQueue;
    [SerializeField] TextMeshProUGUI text;
    bool isEnd;

    // Start is called before the first frame update
    protected override void Start()
    {
        dialogQueue = new Queue<string>();
        text = GetComponentInChildren<TextMeshProUGUI>(true);
        isEnd = false;
        text.enabled = false;
    }

    public void DialogPlay(string input)
    {
        dialogQueue.Enqueue(input);
    }

    private void Update()
    {
        if (dialogQueue.Count > 0 && isEnd == false)
            StartCoroutine(DialogAction());
    }

    IEnumerator DialogAction()
    {
        isEnd = false;
        text.text = dialogQueue.Dequeue();
        text.enabled = true;
        yield return new WaitForSeconds(3);
        isEnd = true;
        text.enabled = false;
    }
}
