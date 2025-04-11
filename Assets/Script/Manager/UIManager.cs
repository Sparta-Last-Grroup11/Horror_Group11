using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
}
