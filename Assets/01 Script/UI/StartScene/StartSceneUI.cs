using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            SceneManager.LoadScene("LobbyScene");
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
