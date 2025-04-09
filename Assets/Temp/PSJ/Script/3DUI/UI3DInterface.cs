using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI3DInterface : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Destroy(gameObject);
    }
}
