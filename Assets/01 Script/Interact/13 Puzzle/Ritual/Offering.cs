using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offering : MonoBehaviour, I_Interactable
{
    [SerializeField] private Ritual ritual;
    [SerializeField] private GameObject lampLight;
    [SerializeField] private ItemData offeringData;
    private bool isActive;
    private void Awake()
    {
        lampLight.SetActive(false);
    }
    public void OnInteraction() //촛불 밝히기
    {
        if (isActive || !GameManager.Instance.player.playerInventory.HasItem(offeringData)) return;

        GameManager.Instance.player.playerInventory.UseItem(offeringData);
        ritual.DoOffer();
        isActive = true;
        lampLight.SetActive(true);
    }
}
