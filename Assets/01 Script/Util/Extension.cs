using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Extension
{
    public static string LanguageTypeToLocalization(LanguageType type)
    {
        switch (type)
        {
            case LanguageType.English:
                return "en";
            case LanguageType.Korean:
                return "kr";
            default:
                return "en";
        }
    }

    public static LanguageType LocalizationToLanguage(string input)
    {
        switch(input)
        {
            case "en":
                return LanguageType.English;
            case "kr":
                return LanguageType.Korean;
            default:
                return LanguageType.English;
        }
    }

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

    public static List<int> RandomUniqueIndices(int min,int max,int count)
    {
        int rangeSize = max - min;
        if (count > rangeSize)
        {
            Debug.LogError("Count cannot be greater than range size");
            return null;
        }
        
        List<int> indices = new List<int>();
        for (int i = min; i <= max; i++) 
            indices.Add(i);

        for (int i = rangeSize - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (indices[i], indices[j]) = (indices[j], indices[i]);
        }

        return indices.GetRange(0, count);
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
