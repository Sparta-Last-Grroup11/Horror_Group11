using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//대화 표출용 UI
public class DialogUI : PopupUI
{
    [SerializeField] TextMeshProUGUI text;
    bool isPlaying = false;

    public override void Init(string input)
    {
        MonologueManager.Instance.isPlaying = true;
        text = GetComponentInChildren<TextMeshProUGUI>(true);
        base.Init(input);
        text.text = input;
        text.enabled = true;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        MonologueManager.Instance.isPlaying = false;
    }
}
