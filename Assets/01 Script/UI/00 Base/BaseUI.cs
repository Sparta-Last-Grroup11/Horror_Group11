using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class BaseUI : MonoBehaviour
{
    [SerializeField] private bool isCursorFree;
    [SerializeField] private LocalizedAsset<TMP_FontAsset> localizedFont;
    [SerializeField] private bool isLocalizedFontByScript = false;
    public Action destroyAction;
    protected CursorLockMode mode;
    [SerializeField] private TextMeshProUGUI[] texts;

    protected virtual void Start()
    {
        StartCoroutine(InitCursorState());
        if (texts.Length <= 0) 
            texts = GetComponentsInChildren<TextMeshProUGUI>(true);
        if(isLocalizedFontByScript)
            SetLocalizedFont();
    }

    private void SetLocalizedFont()
    {
        localizedFont = new LocalizedAsset<TMP_FontAsset>
        {
            TableReference = "Fonts AssetTable",
            TableEntryReference = "UIFont"
        };
    }

    private void OnEnable()
    {
        /*
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;

        Debug.Log($"[Setup] Table: {localizedFont.TableReference}, Entry: {localizedFont.TableEntryReference}");

        localizedFont.LoadAssetAsync().Completed += handle =>
        {
            if (handle.Result != null)
            {
                Debug.Log($"[Font Load] Initial font: {handle.Result.name}");
                ChangeFontInLocalization(handle.Result);
            }
            else
            {
                Debug.LogWarning("[Font Load] Initial font is null");
            }
        };
        */
    }

    private void OnDisable()
    {
        /*
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
        */
    }

    private void OnLocaleChanged(Locale locale)
    {
        // 로케일 바뀔 때 수동으로 다시 로드
        localizedFont.LoadAssetAsync().Completed += handle =>
        {
            if (handle.Result != null)
            {
                ChangeFontInLocalization(handle.Result);
            }
            else
            {
                Debug.LogWarning($"[Font Load] Font for locale {locale.Identifier.Code} is null");
            }
        };
    }

    private void ChangeFontInLocalization(TMP_FontAsset newFont)
    {
        if (newFont == null)
        {
            Debug.LogWarning("NewFont is null. Check localized asset settings, locale bindings, and Addressables.");
            return;
        }

        foreach (var text in texts)
        {
            text.font = newFont;
            Debug.Log($"[Font Change] New Font: {newFont.name}, Text: {text.name}");
        }
    }

    private IEnumerator InitCursorState()
    {
        yield return null; // 한 프레임 대기해서 다른 시스템 초기화 완료 후 실행

        mode = Cursor.lockState;
        Debug.Log("Saved mode: " + mode);

        if (isCursorFree)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    protected virtual void OnDestroy()
    {
        if (isCursorFree)
            Cursor.lockState = mode;
        if (UIManager.IsAlive)
            UIManager.Instance.RemoveUIInList(GetType().Name);
    }

    public virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
}
