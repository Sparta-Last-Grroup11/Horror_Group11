using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITestCOde : MonoBehaviour
{
    [SerializeField] ClipItem item;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            item.OnInteraction();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UIManager.Instance.show<ExampleUI>().Init("Change UI Text");
        }
    }
}
