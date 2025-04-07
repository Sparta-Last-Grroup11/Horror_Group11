using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPuzzle : MonoBehaviour, I_Interactable
{
    [SerializeField] private GameObject puzzle;

    private void Start()
    {
        OnInteraction();
    }
    public void OnInteraction()
    {
        puzzle.SetActive(true);
        LockCursor();
    }

    void LockCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
