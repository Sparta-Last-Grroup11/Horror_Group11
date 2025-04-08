using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPuzzle : MonoBehaviour, I_Interactable
{
    [SerializeField] private GameObject puzzle;

    private void Start() // 테스트용 시작 시 OnInteraction
    {
        OnInteraction();
    }
    public void OnInteraction()
    {
        puzzle.SetActive(true); //퍼즐
        LockCursor();
    }

    void LockCursor() // 시야 고정
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
