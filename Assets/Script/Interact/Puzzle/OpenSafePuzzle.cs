using Unity.VisualScripting;
using UnityEngine;

public class OpenPuzzle : MonoBehaviour, I_Interactable
{
    [SerializeField] private GameObject puzzle;
    [SerializeField] private ControlDoor door;

    public void OnInteraction()
    {
        UIManager.Instance.show<UI3D>().Init(puzzle);
    }

    public void OpenSaveDoor()
    {
        gameObject.layer = 0;
        door.OpenTheDoor();
    }
}
