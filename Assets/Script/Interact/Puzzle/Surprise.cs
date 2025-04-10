using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surprise : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioList;
    [SerializeField] private AudioSource audioSource;
    public void SurpriseSound()
    {
       audioSource.clip = audioList[Random.Range(0, audioList.Length)];
       audioSource.Play();
    }
}
