using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "new ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private E_Item id;
    [SerializeField] private E_ItemType type;
    [SerializeField] private string itemName;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [HideInInspector]
    public int count = 1;
    private GameObject objectIn3D;

    public E_Item Id => id;
    public E_ItemType Type => type;
    public string ItemName { get { return itemName; } set { itemName = value; } }
    public string Description { get { return description; } set { description = value; } }
    public Sprite Icon => icon;
    public GameObject ObjectIn3D { get { return objectIn3D; } set { objectIn3D = value; } }
}
