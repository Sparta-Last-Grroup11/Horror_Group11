using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneController : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.show<Interacting>();
    }
}
