using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.show<StartSceneUI>();
    }
}
