using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class UI3DManager
{
    private GameObject UI3D;

    public UI3DManager()
    {
        UI3D = Resources.Load<GameObject>("UI/UI3D");
    }

    public void Open3DUI(GameObject prefab, string description = "")
    {
        var canvas = UIManager.Instance.mainCanvas;
        var subCam = GameManager.Instance.subCam;

        GameObject uiInstance = Object.Instantiate(UI3D, canvas.transform);
        var obj = Object.Instantiate(prefab, subCam.transform);
        obj.transform.localPosition = new Vector3(0, 0, 1);
        obj.transform.localRotation = Quaternion.identity;
        obj.GetComponent<ItemOnUI>().Init(description);
    }
}
