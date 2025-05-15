using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneController : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.show<Interacting>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && UIManager.Instance.Get<ExitUI>() == null)
            UIManager.Instance.show<ExitUI>();
    }
}
