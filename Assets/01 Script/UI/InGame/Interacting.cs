using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interacting : BaseUI
{
    TextMeshProUGUI text;

    public void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void Start()
    {
        base.Start();
        text.enabled = false;
    }

    public void InteractOn()
    {
        text.enabled = true;
    }

    public void InteractOff()
    {
        text.enabled = false;
    }
}
