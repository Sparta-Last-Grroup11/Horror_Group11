using UnityEngine;

public class ChapterClipBoard : MonoBehaviour, I_Interactable
{
    public void OnInteraction()
    {
        UIManager.Instance.show<ChapterChoiceUI>();
    }
}
