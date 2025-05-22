using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [Header("Glitch")]
    Material glitchMat;
    [SerializeField] float maxGlitch = 50f;
    Coroutine curCourt;

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
        uiList = new Dictionary<string, BaseUI>();
        glitchMat = ResourceManager.Instance.Load<Material>(ResourceType.Material, "ScreenGlitchShader");
        ManagerSetting();
    }

    private void ManagerSetting()
    {
        IsUiActing = false;
        GlitchEnd();
        GameObject obj = GameObject.Find("MainCanvas");
        subCam = GameObject.Find("Sub Camera").GetComponent<Camera>();
        uiCam = GameObject.Find("UI Camera").GetComponent<Camera>();
        if (obj == null)
        {
            mainCanvas = Instantiate(ResourceManager.Instance.Load<GameObject>(ResourceType.UI, "MainCanvas").GetComponent<Canvas>());
            mainCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            mainCanvas.worldCamera = uiCam;
            mainCanvas.planeDistance = 10f;
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
        Debug.Log($"{key} : {uiInstance.name} Instantiate");
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
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GlitchEnd();
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
        prefab.TryGetComponent<Rigidbody>(out Rigidbody rigid);
        if (rigid != null)
            rigid.useGravity = false;
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

    #region 글리치 관련
    public void GlitchStartWithTime(float time, float amount = 50f)
    {
        if (curCourt != null)
            StopCoroutine(curCourt);
        curCourt = StartCoroutine(GlitchEffect(time, amount));
    }

    public void GlitchWithDistance(float ratio)
    {
        glitchMat.SetFloat("_NoiseAmount", maxGlitch * ratio);
        if (ratio < 0.33f)
        {
            glitchMat.SetFloat("_NoiseAmount", 0);
        }
        else
        {
            glitchMat.SetFloat("_GlitchStrength", 1);
        }
    }

    public void GlitchStart(float amount)
    {
        glitchMat.SetFloat("_NoiseAmount", amount);
        glitchMat.SetFloat("_GlitchStrength", 1);
    }

    public void GlitchEnd()
    {
        glitchMat.SetFloat("_NoiseAmount", 0);
        glitchMat.SetFloat("_GlitchStrength", 0);
    }

    IEnumerator GlitchEffect(float time, float amount)
    {
        glitchMat.SetFloat("_NoiseAmount", amount);
        glitchMat.SetFloat("_GlitchStrength", 1);

        yield return new WaitForSeconds(time);

        glitchMat.SetFloat("_NoiseAmount", 0);
        glitchMat.SetFloat("_GlitchStrength", 0);

        curCourt = null;
        yield return null;
    }
    #endregion
}
