using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    [SerializeField] private bool isCursorFree;

    protected virtual void Start()
    {
        if(isCursorFree)
            Cursor.lockState = CursorLockMode.None;
    }

    protected virtual void OnDestroy()
    {
        if (isCursorFree)
            Cursor.lockState = CursorLockMode.Locked;
    }

    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
}
