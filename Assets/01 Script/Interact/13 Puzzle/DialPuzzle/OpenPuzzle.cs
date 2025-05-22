using System.Collections.Generic;
using UnityEngine;
public class OpenPuzzle : MonoBehaviour, I_Interactable
{
    [SerializeField] private GameObject puzzle;
    [SerializeField] private ControlDoor door;
    [SerializeField] private GameObject key;
    private void Start()
    {
       key.SetActive(false); 
    }
    public void OnInteraction()
    {
        UIManager.Instance.show<UI3D>().Init(puzzle, "", false);
        FindObjectOfType<SafeLockPuzzle>().SetSafe(this);
    }

    public void OpenSaveDoor()
    {
        key.SetActive(true);
        gameObject.layer = 0;
        door.Open();
    }
}
