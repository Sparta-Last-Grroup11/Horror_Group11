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
    CursorLockMode mode;

    protected virtual void Start()
    {
        StartCoroutine(InitCursorState());
    }

    private IEnumerator InitCursorState()
    {
        yield return null; // 한 프레임 대기해서 다른 시스템 초기화 완료 후 실행

        mode = Cursor.lockState;
        Debug.Log("Saved mode: " + mode);

        if (isCursorFree)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    protected virtual void OnDestroy()
    {
        if (isCursorFree)
            Cursor.lockState = mode;
        if (UIManager.IsAlive)
            UIManager.Instance.RemoveUIInList(GetType().Name);
    }

    public virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
}
