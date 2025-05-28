using Michsky.UI.Dark;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public enum EndingCategory
{
    Rescued,
    Escape,
    Death,
    NoLife
}

public class EndGameUI : BaseUI
{
    [SerializeField] ModalWindowManager modal;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] Image contentBackground;
    [SerializeField] Button restartBtn;
    [SerializeField] Button quitBtn;
    [SerializeField] private FadePanelUI fadePanel;

    [SerializeField] EndingCategory endGameType;
    [SerializeField] float delayBeforeSceneChange = 1.0f;

    protected override void Start()
    {
        base.Start();
        restartBtn.onClick.AddListener(OnClickReStart);
        quitBtn.onClick.AddListener(OnClickQuit);
    }

    public async void ShowEnding(EndingCategory type, int delayTime)
    {
        SetupVisuals(type);

        await Task.Delay(delayTime);
        UIManager.Instance.IsUiActing = true;
        modal.ModalWindowIn();

        if (type == EndingCategory.NoLife)
            ShowQuitOnly();
        else
            EnableButtons();
    }

    private void SetupVisuals(EndingCategory type)
    {
        Color normalColor = new Color(0.61f, 0.61f, 0.61f, 0.75f);
        Color deathColor = new Color(0.54f, 0.09f, 0.09f, 0.75f);
        Color noLifeColor = new Color(0.33f, 0.104f, 0.255f, 0.75f);
        Color bgColor = normalColor;

        switch (type)
        {
            case EndingCategory.Rescued:
                modal.title = "RESCUED";
                modal.description = "The siren faded away. You have been rescued.";
                break;
                
            case EndingCategory.Escape:
                modal.title = "ESCAPED";
                modal.description = "The door is open. But is it all over?";
                break;

            case EndingCategory.Death:
                modal.title = "YOU DEAD";
                modal.description = $"It's all over. Your story too.\nLives left: {GameManager.Instance.Life}";
                bgColor = deathColor;
                break;
            case EndingCategory.NoLife:
                modal.title = "NO LIVES LEFT";
                modal.description = "No lives remain. Return to the beginning.";
                bgColor = noLifeColor;
                break;
        }

        modal.titleObject = title;
        modal.descriptionObject = description;
        contentBackground.color = bgColor;

        modal.UpdateUI();
    }

    private async void OnClickReStart()
    {
        DisableButtons();
        modal.ModalWindowOut();
        await Task.Delay(100);
        UIManager.Instance.IsUiActing = false;

        StartCoroutine(FadeAndRestartRoutine());
    }

    private IEnumerator FadeAndRestartRoutine()
    {
        fadePanel.FadeOutCanvas();
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.CheckPointLoad();
        Debug.Log("FadeIn 진입1");
        yield return new WaitForSeconds(0.5f);
        fadePanel.FadeInCanvas();
        yield return new WaitForSeconds(1f);
        UIManager.Instance.IsUiActing = false;
        Destroy(gameObject);
    }

    private async void OnClickQuit()
    {
        DisableButtons();
        modal.ModalWindowOut();
        await Task.Delay(100);
        UIManager.Instance.IsUiActing = false;
        await SceneLoadManager.Instance.ChangeScene("StartScene");
    }

    private void ShowQuitOnly()
    {
        restartBtn.gameObject.SetActive(false);
        quitBtn.gameObject.SetActive(true);

        RectTransform quitRect = quitBtn.GetComponent<RectTransform>();
        quitRect.anchorMin = new Vector2(0.5f, quitRect.anchorMin.y);
        quitRect.anchorMax = new Vector2(0.5f, quitRect.anchorMax.y);
        quitRect.pivot = new Vector2(0.5f, 0.5f);
        quitRect.anchoredPosition = new Vector2(-100f, quitRect.anchoredPosition.y);
    }

    private void EnableButtons()
    {
        restartBtn.gameObject.SetActive(true);
        quitBtn.gameObject.SetActive(true);
    }

    private void DisableButtons()
    {
        restartBtn.gameObject.SetActive(false);
        quitBtn.gameObject.SetActive(false);
    }
}
