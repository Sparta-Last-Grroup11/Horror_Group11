using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private bool dontDestroy = true;
    public bool DontDestroy { get { return dontDestroy; } }
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // 씬에서 인스턴스 찾기
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    Debug.LogError($"Singleton<{typeof(T)}> instance not found in the scene.");
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (transform.parent != null) 
            transform.SetParent(null);
        if (_instance == null)
        {
            _instance = this as T;
        }

        if (DontDestroy)
            DontDestroyOnLoad(gameObject);

        else if (_instance != this)
        {
            Debug.LogWarning($"Duplicate Singleton<{typeof(T)}> detected. Destroying duplicate.");
            Destroy(gameObject);
        }
    }
}
