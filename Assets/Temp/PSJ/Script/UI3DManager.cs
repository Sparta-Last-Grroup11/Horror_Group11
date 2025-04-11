using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class UI3DManager
{
    private GameObject UI3D;
    public GameObject CurGameObject;
    public UI3DInterface ui3DInterface;

    public void DestroyUIObject()
    {
        if (CurGameObject != null)
        {
            GameObject.Destroy(CurGameObject);
            CurGameObject = null;
        }
    }

    public UI3DManager()
    {
        UI3D = Resources.Load<GameObject>("UI/UI3D");
    }

    public void Open3DUI(GameObject prefab, string description = "")
    {
        if (UIManager.Instance.IsUiActing)
            return;
        UIManager.Instance.IsUiActing = true;
        var canvas = UIManager.Instance.mainCanvas;
        var subCam = GameManager.Instance.subCam;

        GameObject uiInstance = Object.Instantiate(UI3D, canvas.transform);
        CurGameObject = Object.Instantiate(prefab, subCam.transform);
        CurGameObject.transform.localPosition = new Vector3(0, 0, 1);
        CurGameObject.transform.localRotation = Quaternion.identity;
        CurGameObject.GetComponent<ItemOnUI>().Init(description);
    }
}
