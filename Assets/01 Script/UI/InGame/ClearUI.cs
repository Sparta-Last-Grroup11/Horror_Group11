using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Michsky.UI.Dark;

public class ClearUI : PopupUI
{
    protected override async void Start()
    {
        base.Start();
        UIManager.Instance.IsUiActing = true;
        await GoTosStartScene();
    }

    async Task GoTosStartScene()
    {
        await Task.Delay(1000);
        await SceneLoadManager.Instance.ChangeScene("StartScene");
    }
}
