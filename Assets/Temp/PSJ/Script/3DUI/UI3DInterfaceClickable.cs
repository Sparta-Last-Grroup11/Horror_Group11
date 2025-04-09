using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UI3DInterfaceClickable : MonoBehaviour, IPointerClickHandler
{
    public Camera renderCamera; // RenderTexture를 찍는 카메라
    public RectTransform rawImageRect; // RawImage의 RectTransform

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rawImageRect, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            // RawImage 내 좌표를 0~1로 정규화
            Vector2 normalizedPoint = RectToNormalized(localPoint, rawImageRect);

            // 정규화 좌표 → RenderTexture 픽셀 좌표
            Vector2 textureCoord = new Vector2(
                normalizedPoint.x * renderCamera.targetTexture.width,
                normalizedPoint.y * renderCamera.targetTexture.height
            );

            // 픽셀 좌표 → 카메라 Viewport 좌표 (0~1)
            Vector2 viewportPoint = new Vector2(
                textureCoord.x / renderCamera.targetTexture.width,
                textureCoord.y / renderCamera.targetTexture.height
            );

            // Viewport 좌표로 Ray 생성
            Ray ray = renderCamera.ViewportPointToRay(viewportPoint);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log($"Hit: {hit.collider.name}");
            }
        }
    }

    Vector2 RectToNormalized(Vector2 localPoint, RectTransform rect)
    {
        Vector2 size = rect.rect.size;
        Vector2 pivot = rect.pivot;

        // 로컬 좌표를 0~1 정규화
        float x = localPoint.x / size.x + pivot.x;
        float y = localPoint.y / size.y + pivot.y;

        return new Vector2(x, y);
    }
}
