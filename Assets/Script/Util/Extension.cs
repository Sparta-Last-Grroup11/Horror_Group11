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
}
