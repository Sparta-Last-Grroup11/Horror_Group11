using UnityEngine;
using DG.Tweening;

public class ChapterClipBoard : MonoBehaviour, I_Interactable
{
    PlayerFlash playerFlash;

    private Vector3 targetPos;
    private Quaternion targetRot;
    private Vector3 targetScale = new Vector3(0.5f, 0.5f, 0.5f);

    [SerializeField] private AudioClip clip;

    public void OnInteraction()
    {
        AudioManager.Instance.Audio2DPlay(clip);
        UIManager.Instance.IsUiActing = true;
        targetPos = Camera.main.transform.position + (Camera.main.transform.forward * 0.3f) - (Camera.main.transform.right * 0.1f);
        targetRot = Quaternion.LookRotation(Camera.main.transform.right, -Camera.main.transform.forward);
        
        transform.DOMove(targetPos, 0.5f);
        transform.DORotateQuaternion(targetRot, 0.5f);
        transform.DOScale(targetScale, 0.5f);

        playerFlash = GameManager.Instance.player.GetComponent<PlayerFlash>();
        playerFlash.flashLight.gameObject.SetActive(false);
        GameManager.Instance.player.cantMove = true;

        Invoke("ShowChapterChoiceUI", 0.5f);
    }

    private void ShowChapterChoiceUI()
    {
        UIManager.Instance.show<ChapterChoiceUI>();
    }
}
