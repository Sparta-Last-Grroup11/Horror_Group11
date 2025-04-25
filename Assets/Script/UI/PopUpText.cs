using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpText : PopupUI
{
    [SerializeField] TextMeshProUGUI text;
    // Start is called before the first frame update

    public override void Init(string input = null)
    {
        text = GetComponentInChildren<TextMeshProUGUI>(true);
        text.text = input;
        text.gameObject.SetActive(true);
        base.Init();
    }
}
