using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareSmoke : MonoBehaviour
{
    [SerializeField] private ParticleSystem smokeParticleSystem;
    [SerializeField] private AudioClip smokeClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && smokeParticleSystem.gameObject.activeSelf)
        {
            smokeParticleSystem.Play();
            StartCoroutine(OffSmokeJumpScare());
        }
    }

    IEnumerator OffSmokeJumpScare()
    {
        AudioManager.Instance.Audio3DPlay(smokeClip, smokeParticleSystem.gameObject.transform.position, 1, false, EAudioType.SFX);
        yield return new WaitForSeconds(1f);
        smokeParticleSystem.gameObject.SetActive(false);
    }
}
