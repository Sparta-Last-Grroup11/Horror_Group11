using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenPuzzle : MonoBehaviour, I_Interactable
{
    [SerializeField] private GameObject puzzle;
    [SerializeField] private ControlDoor door;
    private bool isOpend;
    public void OnInteraction()
    {
        if (!isOpend)
        {
            UIManager.Instance.UI3DManager.Open3DUI(puzzle);
            LockCursor();
        }
    }

    void LockCursor() // 시야 고정
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void OpenSaveDoor()
    {
        isOpend = true;
        door.OpenTheDoor();
    }
}
