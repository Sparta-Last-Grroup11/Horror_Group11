using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareSmoke : MonoBehaviour, IJumpScareEvent
{
    private ParticleSystem smokeParticleSystem;
    [SerializeField] private AudioClip smokeClip;

    private void Awake()
    {
        smokeParticleSystem = GetComponent<ParticleSystem>();
    }

    public void TriggerEvent()
    {
        smokeParticleSystem.Play();
        AudioManager.Instance.Audio3DPlay(smokeClip, transform.position);
    }
}
