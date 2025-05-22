using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SkipUI : BaseUI
{
    [SerializeField] Image circle;
    float TargetTime = 1f;
    float curTime;
    bool hasTriggered = false;
    Action onComplete;

    public void Init(float time = 1f, Action callback = null)
    {
        curTime = 0f;
        TargetTime = time;
        hasTriggered = false;
        onComplete = callback;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            curTime += Time.deltaTime;
        }
        else
        {
            curTime -= Time.deltaTime;
        }
        curTime = Mathf.Clamp(curTime, 0, TargetTime);
        circle.fillAmount = curTime / TargetTime;

        if (!hasTriggered && curTime >= TargetTime)
        {
            hasTriggered = true;
            onComplete?.Invoke();
            Destroy(gameObject);
        }
    }
}
