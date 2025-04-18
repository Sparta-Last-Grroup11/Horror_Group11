using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "new ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private E_Item id;
    [SerializeField] private E_ItemType type;
    [SerializeField] private string itemName;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;

    public E_Item Id => id;
    public E_ItemType Type => type;
    public string ItemName => itemName;
    public string Description => description;
    public Sprite Icon => icon;
}
