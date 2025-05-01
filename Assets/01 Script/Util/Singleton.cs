using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected virtual bool dontDestroy => true;
    private static T _instance;
    public static bool IsAlive;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                IsAlive = true;
                Create();
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        Create();

        if (_instance != this)
        {
            Destroy(gameObject);
        }
        else if (dontDestroy) 
        {
            DontDestroyOnLoad(this);
        }
    }

    protected static void Create()
    {
        if (_instance == null)
        {
            T[] objects = FindObjectsOfType<T>();

            if (objects.Length > 0)
            {
                _instance = objects[0];

                for (int i = 1; i < objects.Length; ++i)
                {
                    if (Application.isPlaying)
                    {
                        Destroy(objects[i].gameObject);
                    }
                    else
                    {
                        DestroyImmediate(objects[i].gameObject);
                    }
                }
            }
            else
            {
                GameObject go = new GameObject(string.Format("{0}", typeof(T).Name));
                _instance = go.AddComponent<T>();
            }
        }
    }

    protected virtual void OnDestroy()
    {
        _instance = null;
        IsAlive = false;
    }
}
