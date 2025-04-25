using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemCount;
    public string description;
    [SerializeField] TextMeshProUGUI descriptionPanel;
    public GameObject Object3D;

    public void Init(Sprite image,string name,int count, string description)
    {
        itemImage.sprite = image;
        itemName.text = name;
        itemCount.text = count.ToString();
        this.description = description;
    }
}
