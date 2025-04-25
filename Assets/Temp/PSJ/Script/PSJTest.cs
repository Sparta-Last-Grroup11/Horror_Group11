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
        DialogUI dialog = UIManager.Instance.Get<DialogUI>();
        dialog.DialogPlay("Example in Dialog");
        dialog.DialogPlay("See Change In Dialog");
        dialog.DialogPlay("DialogEnd");
    }

    private void PopUpTest()
    {
        UIManager.Instance.show<PopUpText>().Init("Test");
    }
}
