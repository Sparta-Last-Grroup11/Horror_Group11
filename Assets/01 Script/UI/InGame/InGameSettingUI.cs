using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameSettingUI : BaseUI
{
    [SerializeField] Button helpButton;
    [SerializeField] Button SettingButton;
    [SerializeField] Button ExitButton;

    // Start is called before the first frame update
    protected override void Start()
    {
        mode = Cursor.lockState;
        Cursor.lockState = CursorLockMode.None;
        UIManager.Instance.IsUiActing = true;
        helpButton.onClick.AddListener(() => ShowUIInGameSetting<HelpUI>());
        SettingButton.onClick.AddListener(()=> ShowUIInGameSetting<SettingUI>());
        ExitButton.onClick.AddListener(()=> ShowUIInGameSetting<ExitUI>());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = mode;
            UIManager.Instance.IsUiActing = false;
            Destroy(gameObject);
        }
    }

    private void ShowUIInGameSetting<T>() where T : BaseUI
    {
        Cursor.lockState = CursorLockMode.Locked;
        UIManager.Instance.show<T>();
        Destroy(gameObject);
    }

    protected override void OnDestroy()
    {
        
    }
}
