using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Extension
{
    public static T StringToEnum<T>(string typename) where T : struct, Enum
    {
        if (Enum.TryParse(typename, true, out T result))
        {
            return result;
        }
        else
        {
            Debug.LogWarning($"[StringToEnum] Unknown Enum Value: {typename}, returning default ({default(T)})");
            return default;
        }
    }

    public static Vector3 FloatToVector3(this float[] arr)
    {
        if (arr == null || arr.Length < 3)
        {
            Debug.LogWarning("배열이 null이거나 길이가 3보다 작습니다.");
            return Vector3.zero;
        }

        return new Vector3(arr[0], arr[1], arr[2]);
    }

    public static Quaternion FloatToQuaternion(this float[] arr)
    {
        if (arr == null || arr.Length < 3)
        {
            Debug.LogWarning("배열이 null이거나 길이가 3보다 작습니다.");
            return Quaternion.identity;
        }

        return Quaternion.Euler(arr[0], arr[1], arr[2]);
    }

    public static void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (obj == null) return;

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    public static void onClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
