using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Michsky.UI.Dark;

public class ExitUI : BaseUI
{
    [SerializeField] ModalWindowManager myModalWindow;
    [SerializeField] Button exitButton;
    [SerializeField] Button cancelButton;
    [SerializeField] Animator animator;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        UIManager.Instance.IsUiActing = true;
        exitButton.onClick.AddListener(async () => await SceneLoadManager.Instance.ChangeScene("StartScene"));
        cancelButton.onClick.AddListener(async () => await DestroyAction());
        myModalWindow.ModalWindowIn();
    }

    private async void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            await DestroyAction();
    }

    async Task DestroyAction()
    {
        myModalWindow.ModalWindowOut();
        await Task.Delay(500);
        UIManager.Instance.IsUiActing = false;
        Destroy(gameObject);
    }
}
