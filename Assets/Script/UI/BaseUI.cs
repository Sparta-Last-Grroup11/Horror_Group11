using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    [SerializeField] private bool isCursorFree;
    public Action destroyAction;

    protected virtual void Start()
    {
        if(isCursorFree)
            Cursor.lockState = CursorLockMode.None;
    }

    protected virtual void OnDestroy()
    {
        if (isCursorFree)
            Cursor.lockState = CursorLockMode.Locked;
        if (UIManager.Instance != null)
            UIManager.Instance.RemoveUIInList(GetType().Name);
    }

    public virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
}
