using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    [SerializeField] private bool isCursorFree;

    protected virtual void Start()
    {
        if(isCursorFree)
            Cursor.lockState = CursorLockMode.None;
    }

    protected virtual void DestroySelf()
    {
        if(isCursorFree)
            Cursor.lockState = CursorLockMode.Locked;
        Destroy(gameObject);
    }
}
