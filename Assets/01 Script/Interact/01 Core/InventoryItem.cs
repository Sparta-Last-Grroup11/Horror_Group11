using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour, I_Interactable
{
    [SerializeField] private AudioClip zombieScream;
    [SerializeField] private AudioClip seriesSound;

    [SerializeField] ItemData itemData;
    public ItemData ItemData => itemData;
    [SerializeField] private int commentID = -1;
    [SerializeField] private int questID = -1;
    
    public virtual void OnInteraction()
    {
        ItemData newItemData = Instantiate(itemData);
        newItemData.name = itemData.name;
        newItemData.ItemName = itemData.ItemName;
        newItemData.Description = itemData.Description;
        newItemData.ObjectIn3D = ResourceManager.Instance.Load<GameObject>(ResourceType.Item, this.name);
        GameManager.Instance.player.playerInventory.AddItem(newItemData);
        AudioManager.Instance.Audio2DPlay(zombieScream, 1, false, EAudioType.SFX);

        if(commentID != -1)
        {
            MonologueManager.Instance.DialogPlay(commentID);
        }
        QuestManager.Instance.QuestTrigger(questID);
        Destroy(gameObject);
    }
}
