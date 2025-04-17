using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.WebCam;

public class UIManager : Singleton<UIManager>
{
    [Header("No Need to Allocate")]
    private Dictionary<string, BaseUI> uiList;
    public Canvas mainCanvas;
    public bool IsUiActing;
    public BaseUI CurUI3D;
    public GameObject cur3DObject;

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
        uiList = new Dictionary<string, BaseUI>();
        mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
    }

    public T show<T>() where T : BaseUI
    {
        string key = typeof(T).Name;
        var uiPrefab = ResourceManager.Instance.Load<T>(ResourceType.UI, typeof(T).Name);
        var uiInstance = Instantiate(uiPrefab, mainCanvas.transform);
        uiList[key] = uiInstance;
        return uiInstance;
    }

    public T Get<T>() where T: BaseUI
    {
        string key = typeof(T).Name;
        if (uiList.TryGetValue(key,out BaseUI ui))
        {
            return ui as T;
        }
        Debug.LogWarning($"UI Not Found: {key}");
        return null;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ClearList();
    }

    public void ClearList()
    {
        uiList.Clear();
    }

    public void RemoveUIInList(string name)
    {
        uiList.Remove(name);
        Debug.Log($"{name} is Delete");
        if (!uiList.Remove(name))
            Debug.LogWarning($"UIManager: {name} is Delete");
        else
            Debug.LogWarning($"UIManager: {name} is Not Found In List");

    }

    #region 3D관련
    public GameObject MakePrefabInSubCam(GameObject obj)
    {
        GameObject prefab = Instantiate(obj, GameManager.Instance.subCam.transform);
        prefab.transform.localPosition = new Vector3(0, 0, 1);
        prefab.transform.LookAt(GameManager.Instance.subCam.transform);
        cur3DObject = prefab;
        return prefab;
    }

    public void RemovePrefabInSumCam()
    {
        if (cur3DObject == null)
            return;
        Destroy(cur3DObject);
        cur3DObject = null;
    }
    #endregion
}
