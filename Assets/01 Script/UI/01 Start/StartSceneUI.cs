using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Extension;

public class StartSceneUI : PopupUI
{
    [SerializeField] Button startBut;
    [SerializeField] Button SettingBut;
    [SerializeField] Button ExitBut;
    [SerializeField] DoubleDoor door;
    [SerializeField] AudioClip buttonClickSound;

    protected override void Start()
    {
        base.Start();
        door = GameObject.FindAnyObjectByType<DoubleDoor>();

        startBut.onClick.AddListener(async () =>
        {
            startBut.interactable = false;
            door.OnInteraction();
            await Task.Delay(1000);
            UIManager.Instance.show<PrologueUI>();
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
