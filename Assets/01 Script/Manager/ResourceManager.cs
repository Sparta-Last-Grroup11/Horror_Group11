using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum ResourceType
{
    None,
    JsonData,
    Item,
    Player,
    Sound,
    UI,
    Material,
    Enemy
}

public class ResourceManager : Singleton<ResourceManager>
{
    public Dictionary<string, object> assetPool = new Dictionary<string, object>();

    public T Load<T>(ResourceType type, string name) where T : UnityEngine.Object
    {
        T handle = default;
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogWarning("[ResourceManager] Load failed: name is null or empty.");
            return null;
        }

        string path = (type == ResourceType.None) ? name : $"{type}/{name}";

        if (!assetPool.ContainsKey(path))
        {
            var obj = Resources.Load<T>(path);
            if (obj == null)
            {
                Debug.LogError($"[ResourceManager] Failed to load GameObject at path: Resources/{path}");
                return null;
            }
            assetPool.Add(path, obj);
        }

        handle = (T)assetPool[path];

        return handle;
    }

    public async Task<T> LoadAsync<T>(ResourceType type, string name) where T : UnityEngine.Object
    {
        T handle = default;

        var path = (type == ResourceType.None) ? name : $"{type}/{name}";

        if (!assetPool.ContainsKey(path))
        {
            var op = Resources.LoadAsync<T>(path);

            while (!op.isDone)
            {
                await Task.Yield();
            }

            var obj = op.asset;

            if (obj == null)
                return default;

            assetPool.Add(path, obj);
        }

        handle = (T)assetPool[path];

        return handle;
    }
}
