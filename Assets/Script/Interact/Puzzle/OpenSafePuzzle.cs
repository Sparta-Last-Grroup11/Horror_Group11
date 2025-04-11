using UnityEngine;

public class OpenPuzzle : MonoBehaviour, I_Interactable
{
    [SerializeField] private GameObject puzzle;
    [SerializeField] private ControlDoor door;

    public void OnInteraction()
    {
            UIManager.Instance.UI3DManager.Open3DUI(puzzle);
            LockCursor();
    }

    void LockCursor() // 시야 고정
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void OpenSaveDoor()
    {
        gameObject.layer = 0;
        door.OpenTheDoor();
    }
}
