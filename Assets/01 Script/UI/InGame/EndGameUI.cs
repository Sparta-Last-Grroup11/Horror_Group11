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

    private EndingCategory currentEnding;

    public async void ShowEnding(EndingCategory type)
    {
        currentEnding = type;
        SetupVisuals(type);

        await Task.Delay(1000);
        modal.ModalWindowIn();
        await Task.Delay((int)(delayBeforeSceneChange * 4000));
        modal.ModalWindowOut();
        await SceneLoadManager.Instance.ChangeScene("StartScene");

    }

    private void SetupVisuals(EndingCategory type)
    {
        Color normalColor = new Color(0.61f, 0.61f, 0.61f, 0.75f);
        Color deathColor = new Color(0.54f, 0.09f, 0.09f, 0.75f);

        switch (type)
        {
            case EndingCategory.Rescued:
                modal.title = "Rescued";
                modal.description = "The siren faded away. You have been rescued.";
                contentBackground.color = normalColor;
                break;

            case EndingCategory.AnotherWorld:
                modal.title = "ANOTHER WORLD";
                modal.description = "Endless space opened up. Maybe… you won't be coming back.";
                contentBackground.color = normalColor;
                break;
                
            case EndingCategory.Escape:
                modal.title = "ESCAPED";
                modal.description = "The door is open. But is it all over?";
                contentBackground.color = normalColor;
                break;

            case EndingCategory.Death:
                modal.title = "YOU DEAD";
                modal.description = "It's all over. Your story too.";
                contentBackground.color = deathColor;
                break;
        }

        modal.titleObject = title;
        modal.descriptionObject = description;

        modal.UpdateUI();
    }
}
