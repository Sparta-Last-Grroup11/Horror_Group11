using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

public class ShadowExclude : MonoBehaviour
{
    [MenuItem("Tools/Disable Shadows Except Selected and Children")]
    static void DisableAllShadowsExceptSelected()
    {
        if (Selection.activeGameObject == null)
        {
            Debug.LogWarning("선택한 오브젝트가 없습니다.");
            return;
        }

        GameObject root = Selection.activeGameObject;

        // 예외 대상: 선택한 오브젝트와 자식들
        Transform[] exceptionTransforms = root.GetComponentsInChildren<Transform>(true);

        var allRenderers = GameObject.FindObjectsOfType<MeshRenderer>(true);
        int affectedCount = 0;

        foreach (var renderer in allRenderers)
        {
            bool isException = false;

            foreach (var t in exceptionTransforms)
            {
                if (renderer.transform == t)
                {
                    isException = true;
                    break;
                }
            }

            if (!isException)
            {
                renderer.shadowCastingMode = ShadowCastingMode.Off;
                affectedCount++;
            }
        }

        Debug.Log($"예외 대상 제외, 총 {affectedCount}개의 오브젝트 그림자 꺼짐.");
    }
}