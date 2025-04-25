using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.show<StartSceneUI>();
    }
}
