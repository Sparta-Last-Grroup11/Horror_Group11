using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public Dictionary<string, BaseUI> UIList = new Dictionary<string, BaseUI>();

    public T LoadUI<T>() where T : BaseUI
    {
        if(UIList.ContainsKey(typeof(T).Name))
        {
            Debug.Log("딕셔너리에 발견");
            return UIList[typeof(T).Name] as T;
        }

        var ui = Resources.Load<BaseUI>($"UI/{typeof(T).Name}") as T;
        UIList.Add(ui.name, ui);
        return ui;
    }
}
