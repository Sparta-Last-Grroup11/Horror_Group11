using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Extension;

public class StartSceneUI : BaseUI
{
    [SerializeField] Button startBut;
    [SerializeField] Button SettingBut;
    [SerializeField] Button ExitBut;

    protected override void Start()
    {
        startBut.onClick.AddListener(() =>
        {
            //씬 이동 추후 구현
        });

        SettingBut.onClick.AddListener(() =>
        {
            UIManager.Instance.show<SettingUI>();
        });

        ExitBut.onClick.AddListener(() =>
        {
            onClickExit();      
        });
    }
}
