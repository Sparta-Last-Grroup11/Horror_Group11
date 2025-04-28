using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    private void Awake()
    {
        StageManager.Instance.StageMake();
    }
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.show<HeartBeat>();
        UIManager.Instance.show<GlitchUI>();
        UIManager.Instance.show<Interacting>();
    }
}
