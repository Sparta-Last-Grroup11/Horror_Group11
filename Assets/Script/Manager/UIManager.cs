using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Canvas mainCanvas;
    public UI3DManager UI3DManager;
    public bool IsUiActing;

    protected override void Awake()
    {
        base.Awake();
        mainCanvas = Instantiate(Resources.Load<GameObject>("UI/MainCanvas")).GetComponent<Canvas>();
        UI3DManager = new UI3DManager();
    }

    public T show<T>() where T : BaseUI
    {
        var ui = ResourceManager.Instance.LoadUI<T>();
        return Instantiate(ui, mainCanvas.transform);
    }
}
