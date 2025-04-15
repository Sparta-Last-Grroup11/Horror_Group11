using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class UIManager : Singleton<UIManager>
{
    [Header("No Need to Allocate")]
    public Canvas mainCanvas;
    public bool IsUiActing;
    public BaseUI CurUI3D;
    public GameObject cur3DObject;

    protected override void Awake()
    {
        base.Awake();
        mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
    }

    public T show<T>() where T : BaseUI
    {
        var ui = ResourceManager.Instance.LoadUI<T>();
        return Instantiate(ui, mainCanvas.transform);
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
