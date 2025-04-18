using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingUI : PopupUI
{
    protected override void Start()
    {
        base.Start();
        UIManager.Instance.ClearListAndDestroy(this);
    }
}
