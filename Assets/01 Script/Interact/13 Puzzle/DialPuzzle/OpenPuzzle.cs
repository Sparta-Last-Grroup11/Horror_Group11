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
    public void OnInteraction() //상호작용 시 퍼즐 UI 생성
    {
        UIManager.Instance.show<UI3D>().Init(puzzle, null, UIManager.Instance.show<SafeHelp>(), false);
        FindObjectOfType<SafeLockPuzzle>().SetSafe(this);
    }

    public void OpenSaveDoor() //퍼즐 해결 시 오픈
    {
        key.SetActive(true);
        gameObject.layer = 0;
        door.Open();
    }
}
