using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    None,
    JsonData,
    Item,
    Player,
    Sound,
    UI,
    Material
}
public class ResourceManager : Singleton<ResourceManager>
{
    public Dictionary<string, BaseUI> UIList = new Dictionary<string, BaseUI>();

    public T Load<T>(ResourceType type, string name) where T : UnityEngine.Object
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogWarning("[ResourceManager] Load failed: name is null or empty.");
            return null;
        }

        string path = (type == ResourceType.None) ? name : $"{type}/{name}";
        T obj = Resources.Load<T>(path);

        if (obj == null)
        {
            Debug.LogError($"[ResourceManager] Failed to load GameObject at path: Resources/{path}");
            return null;
        }

        return obj;
    }
}
