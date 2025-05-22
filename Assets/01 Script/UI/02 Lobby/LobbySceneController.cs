using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneController : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.show<Interacting>();
        Invoke(nameof(ShowHowtoControl), 1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && UIManager.Instance.Get<ExitUI>() == null)
            UIManager.Instance.show<ExitUI>();
    }

    void ShowHowtoControl()
    {
        UIManager.Instance.show<HelpUI>().ActivePanel(0);
        StartCoroutine(HelpUIExit());
    }

    IEnumerator HelpUIExit()
    {
        while (UIManager.Instance.Get<HelpUI>() != null)
        {
            yield return null;
        }
        UIManager.Instance.show<PopUpText>().Init("You Can see this Panel in Exit - Help");
        AudioManager.Instance.Audio2DPlay(ResourceManager.Instance.Load<AudioClip>(ResourceType.Sound, "2D/UI/ButtonClick"));
        yield return null;
    }
}
