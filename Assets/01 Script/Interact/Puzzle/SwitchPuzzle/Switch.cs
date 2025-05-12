using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, I_Interactable
{
    [SerializeField] private bool isOn = false;
    [SerializeField] private SwitchPuzzle puzzle;
    [SerializeField] private int id;
    [SerializeField] private Renderer renderer;
    private Color origin;
    private void Awake()
    {
        puzzle.SetDictionary(id, this);
        renderer = GetComponent<Renderer>();
        origin = renderer.material.color;
    }
    public void OnInteraction()
    {
        puzzle.TriggerSwitch(id);
    }

    public void ChangeIsOn()
    {
        isOn = !isOn;
        if (isOn)
        {
            puzzle.ChangeCount(1);
            renderer.material.color = new Color(50/255f, 200/255f, 50/255f);
        }
        else
        {
            puzzle.ChangeCount(-1);
            renderer.material.color = origin;
        }
    }

    public void DownPuzzle()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void ResetSwitch()
    {
        if (isOn) ChangeIsOn();
    }
}
