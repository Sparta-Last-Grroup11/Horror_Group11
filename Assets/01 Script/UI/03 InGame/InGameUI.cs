using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.show<HeartBeat>();
        UIManager.Instance.show<RamainFlashBatteryUI>();
        UIManager.Instance.show<Interacting>();
        StageManager.Instance.StageMake();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !UIManager.Instance.IsUiActing && UIManager.Instance.Get<InGameSettingUI>() == null )
            UIManager.Instance.show<InGameSettingUI>();
    }
}
