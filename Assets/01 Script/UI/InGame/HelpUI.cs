using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpUI : BaseUI
{
    protected override void Start()
    {
        base.Start();
        UIManager.Instance.IsUiActing = true;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        UIManager.Instance.IsUiActing = false;
    }
}
