using Michsky.UI.Dark;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;

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
    [SerializeField] EndingCategory endGameType;
    [SerializeField] float delayBeforeSceneChange = 3.0f;

    private EndingCategory currentEnding;

    public async void ShowEnding(EndingCategory type)
    {
        currentEnding = type;
        SetupVisuals(type);

        modal.ModalWindowIn();
        await Task.Delay((int)(delayBeforeSceneChange * 1000));
        modal.ModalWindowOut();
        await Task.Delay(1000);

        await SceneLoadManager.Instance.ChangeScene("StartScene");
    }

    private void SetupVisuals(EndingCategory type)
    {
        switch (type)
        {
            case EndingCategory.Rescued:
                modal.title = "Rescued";
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
                break;
        }

        modal.titleObject = title;
        modal.descriptionObject = description;

        modal.UpdateUI();
    }
}
