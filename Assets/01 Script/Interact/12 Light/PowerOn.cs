using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PowerOn : MonoBehaviour, I_Interactable
{
    [SerializeField] private bool isAct;
    [SerializeField] private LightStateSO lightState;
    [SerializeField] private AudioClip clip;
    [SerializeField] private int questID = 4;
    [SerializeField] private GameObject particle;
    [SerializeField] private Light lamp;
    private PlayableDirector playableDirector;

    private void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
        lightState.ResetLight();
        particle.SetActive(false);
        lamp.enabled = false;
    }

    public void OnInteraction()
    {
        if (isAct) return;
        isAct = true;
        if(clip != null)
            AudioManager.Instance.Audio2DPlay(clip);
        lightState.OnPower();
        QuestManager.Instance.QuestTrigger(questID);
        playableDirector.Play();
        GameManager.Instance.player.cantMove = true;
        if(lamp!=null)
            lamp.enabled = true;
        Invoke("PlayerCanMove", 3f);
    }

    private void PlayerCanMove()
    {
        GameManager.Instance.player.cantMove = false;
        
        StartCoroutine(Spark());
    }

    IEnumerator Spark()
    {
        particle.SetActive(true);
        yield return new WaitForSeconds(2f);
        particle.SetActive(false);
    }
}
