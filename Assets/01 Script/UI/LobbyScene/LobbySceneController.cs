using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneController : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.show<Interacting>();
    }
}
