using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Michsky.UI.Dark;
using UnityEngine.UI;

public class TelephoneUI : PopupUI
{
    private ModalWindowManager modalWindowManager;

    private void Awake()
    {
        modalWindowManager = GetComponent<ModalWindowManager>();
    }

    protected override async void Start()
    {
        base.Start();
        UIManager.Instance.IsUiActing = true;
        modalWindowManager.ModalWindowIn();
        
        await GoTosStartScene();
    }

    async Task GoTosStartScene()
    {
        await Task.Delay(3000);
        await SceneLoadManager.Instance.ChangeScene("StartScene");
    }
}
