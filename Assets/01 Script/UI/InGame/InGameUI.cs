using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.show<HeartBeat>();
        UIManager.Instance.show<Interacting>();
        StageManager.Instance.StageMake();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !UIManager.Instance.IsUiActing && UIManager.Instance.Get<InGameSettingUI>() == null )
            UIManager.Instance.show<InGameSettingUI>();
    }
}
