using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareAlarmClockTrigger : MonoBehaviour
{
    private float stayTime;
    private float triggerTime = 5f;

    private BoxCollider triggerBox;
    [SerializeField] private GameObject alarmClock;
    [SerializeField] private AudioClip alarmClockSoundClip;

    private void Awake()
    {
        triggerBox = GetComponent<BoxCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stayTime += Time.deltaTime;
            if (stayTime > triggerTime)
            {
                AudioManager.Instance.Audio3DPlay(alarmClockSoundClip, alarmClock.transform.position, 1, false, EAudioType.SFX);
                StartCoroutine(alarmJumpScareOFF());
            }
        }
    }

    IEnumerator alarmJumpScareOFF()
    {
        yield return new WaitForSeconds(5f);
        triggerBox.enabled = false;
    }
}
