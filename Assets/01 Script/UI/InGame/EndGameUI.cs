using Michsky.UI.Dark;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;

public enum EndingCategory
{
    Rescued,
    AnotherWorld,
    Escape,
    Death
}

public class EndGameUI : BaseUI
{
    [SerializeField] ModalWindowManager modal;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] Image contentBackground;
    [SerializeField] EndingCategory endGameType;
    [SerializeField] float delayBeforeSceneChange = 3.0f;

    public async void ShowEnding(EndingCategory type, int delayTime)
    {
        SetupVisuals(type);

        await Task.Delay(delayTime);
        modal.ModalWindowIn();
        await Task.Delay((int)(delayBeforeSceneChange * 3000));
        modal.ModalWindowOut();
        await SceneLoadManager.Instance.ChangeScene("StartScene");
    }

    private void SetupVisuals(EndingCategory type)
    {
        Color normalColor = new Color(0.61f, 0.61f, 0.61f, 0.75f);
        Color deathColor = new Color(0.54f, 0.09f, 0.09f, 0.75f);
        Color bgColor = normalColor;

        switch (type)
        {
            case EndingCategory.Rescued:
                modal.title = "RESCUED";
                modal.description = "The siren faded away. You have been rescued.";
                break;

            case EndingCategory.AnotherWorld:
                modal.title = "ANOTHER WORLD";
                modal.description = "Endless space opened up. Maybe… you won't be coming back.";
                break;
                
            case EndingCategory.Escape:
                modal.title = "ESCAPED";
                modal.description = "The door is open. But is it all over?";
                break;

            case EndingCategory.Death:
                modal.title = "YOU DEAD";
                modal.description = "It's all over. Your story too.";
                bgColor = deathColor;
                break;
        }

        modal.titleObject = title;
        modal.descriptionObject = description;
        contentBackground.color = bgColor;

        modal.UpdateUI();
    }
}
