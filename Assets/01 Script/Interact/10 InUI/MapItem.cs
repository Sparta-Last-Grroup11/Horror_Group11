using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//지도를 상호작용했을 때 지도 오브젝트가 3D UI에 띄워지도록 만듬.
//자기 자신을 전달해 UIManager에서 UI에 띄워지기 위한 필요 설정들을 추가하여 복제하여 화면에 띄움.
public class MapItem : MonoBehaviour, I_Interactable
{
    public void OnInteraction()
    {
        UIManager.Instance.show<UI3D>().Init(gameObject, null, UIManager.Instance.show<UI3DHelp>(), true, 2);
    }
}
