using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//3D UI상에서 보여지는 클립보드의 스크립트. 텍스트에 매개 변수로 들어온 문자열을 넣는다.
public class ClipBoardInUI : ItemOnUI
{
    TextMeshProUGUI text;

    public override void Init(string input)
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = input;
    }
}
