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
        mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        UI3DManager = new UI3DManager();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
