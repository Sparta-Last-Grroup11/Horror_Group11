using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ritual : MonoBehaviour, I_Interactable
{
    [SerializeField] private GameObject key;
    private int countActive = 0;
    private void Awake()
    {
        key.SetActive(false);
    }
    public void OnInteraction()
    {
        if (countActive == 5)
        {
            key.SetActive(true);
            countActive = 0;
            MonologueManager.Instance.DialogPlay(12);
        }
    }

    public void DoOffer()
    {
        countActive++;
    }
}
