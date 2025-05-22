using Michsky.UI.Dark;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class HelpUI : BaseUI
{
    [SerializeField] ModalWindowManager modal;
    [SerializeField] List<Button> buttons;
    [SerializeField] List<GameObject> panels;
    [SerializeField] Button exitButton;

    protected override void Start()
    {
        base.Start();
        UIManager.Instance.IsUiActing = true;
        modal.ModalWindowIn();
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => ActivePanel(index));
            Debug.Log($"{index}Button Active");
        }
        exitButton.onClick.AddListener(async () => await DestroyAction());
    }


    private async void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            await DestroyAction();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    async Task DestroyAction()
    {
        modal.ModalWindowOut();
        Cursor.lockState = CursorLockMode.Locked;
        await Task.Delay(500);
        UIManager.Instance.IsUiActing = false;
        Destroy(gameObject);
    }

    public void ActivePanel(int num)
    {
        for (int i = 0; i < panels.Count; i++)
        {
            if (i == num)
                panels[i].SetActive(true);
            else
                panels[i].SetActive(false);
        }
    }
}
