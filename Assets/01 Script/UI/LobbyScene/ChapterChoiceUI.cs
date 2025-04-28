using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChapterChoiceUI : BaseUI
{
    [SerializeField] Button chapter1BTN;

    private void OnEnable()
    {
        UIManager.Instance.IsUiActing = true;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        UIManager.Instance.IsUiActing = false;
    }

    protected override void Start()
    {
        base.Start();
        chapter1BTN.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameScene");
        }
        );
    }
}
