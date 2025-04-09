using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class ItemOnUI : MonoBehaviour
{
    public float rotationSpeed = 5f;

    private bool isDragging = false;
    private Vector3 lastMousePosition;

    public virtual void Init(string input = "")
    {
    }

    protected virtual void Update()
    {
        // 마우스 왼쪽 버튼을 누르면 드래그 시작
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        // 마우스 버튼 떼면 드래그 종료
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // 드래그 중일 때 회전 처리
        if (isDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            float rotationY = -delta.x * rotationSpeed * Time.deltaTime;

            transform.Rotate(0, rotationY, 0f, Space.Self);

            lastMousePosition = Input.mousePosition;
        }
    }
}
