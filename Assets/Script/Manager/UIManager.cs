using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.WebCam;
using static Extension;

public class UIManager : Singleton<UIManager>
{
    [Header("No Need to Allocate")]
    private Dictionary<string, BaseUI> uiList;
    public Canvas mainCanvas;
    public bool IsUiActing;
    public BaseUI CurUI3D;
    public GameObject cur3DObject;
    public Camera subCam;
    public Camera uiCam;

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
        uiList = new Dictionary<string, BaseUI>();
        ManagerSetting();
    }

    private void ManagerSetting()
    {
        GameObject obj = GameObject.Find("MainCanvas");
        subCam = GameObject.Find("Sub Camera").GetComponent<Camera>();
        uiCam = GameObject.Find("UI Camera").GetComponent<Camera>();
        if (obj == null)
        {
            mainCanvas = Instantiate(ResourceManager.Instance.Load<GameObject>(ResourceType.UI, "MainCanvas").GetComponent<Canvas>());
            mainCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            mainCanvas.worldCamera = uiCam;
            mainCanvas.planeDistance = 900f;
        }
        else
        {
            mainCanvas = obj.GetComponent<Canvas>();
            mainCanvas.worldCamera = uiCam;
        }
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
        ManagerSetting();
    }

    public void ClearList()
    {
        uiList.Clear();
    }

    public void ClearListAndDestroy(BaseUI script = null)
    {
        foreach(var ui in uiList)
        {
            if (ui.Value != null && ui.Value.gameObject != null)
            {
                if (ui.Value != script) 
                    Destroy(ui.Value.gameObject);
            }
        }
        ClearList();
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
        if (cur3DObject != null)
        {
            Destroy(cur3DObject.gameObject);
            cur3DObject = null;
        }    
        GameObject prefab = Instantiate(obj, subCam.transform);
        prefab.transform.localPosition = new Vector3(0, 0, 1);
        prefab.transform.LookAt(subCam.transform);
        SetLayerRecursively(prefab, LayerMask.NameToLayer("UIItem"));
        prefab.layer = LayerMask.NameToLayer("UIItem");
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
