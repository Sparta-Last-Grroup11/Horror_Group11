using System.Collections.Generic;
using UnityEngine;
public class OpenPuzzle : MonoBehaviour, I_Interactable
{
    [SerializeField] private GameObject puzzle;
    [SerializeField] private ControlDoor door;

    public void OnInteraction()
    {
        UIManager.Instance.show<UI3D>().Init(puzzle);
        FindObjectOfType<SafeLockPuzzle>().SetSafe(this);
    }

    public void OpenSaveDoor()
    {
        gameObject.layer = 0;
        door.Open();
    }
}
