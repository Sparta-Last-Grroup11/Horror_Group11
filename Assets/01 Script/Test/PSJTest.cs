using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSJTest : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(DialogTest), 3.0f);
        Invoke(nameof(PopUpTest), 4.0f);
    }

    private void Update()
    {

    }

    private void DialogTest()
    {
        MonologueManager.Instance.DialogPlay("TestString");
        MonologueManager.Instance.DialogPlay("TestString2");
        MonologueManager.Instance.DialogPlay("TestString3");
    }

    private void PopUpTest()
    {
        UIManager.Instance.show<PopUpText>().Init("Test");
    }
}
