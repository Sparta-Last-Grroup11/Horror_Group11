using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareSinkTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem waterParticle;
    [SerializeField] private AudioClip waterSound;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player")) return;

        triggered = true;

        waterParticle.Play();
        AudioManager.Instance.Audio3DPlay(waterSound, transform.position);
    }

}
