using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClipBoardInUI : ItemOnUI
{
    TextMeshProUGUI text;

    public override void Init(string input)
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = input;
    }
}
