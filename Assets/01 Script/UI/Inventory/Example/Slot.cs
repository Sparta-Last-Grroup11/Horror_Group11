using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI itemCount;
    public GameObject Object3D;

    public void Init(Sprite image,string name,int count)
    {
        itemImage.sprite = image;
        itemName.text = name;
        itemCount.text = count.ToString();
    }
}
