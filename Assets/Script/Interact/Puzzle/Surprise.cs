using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surprise : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioList;
    public void SurpriseSound()
    {
      AudioManager.Instance.Audio3DPlay(audioList[Random.Range(0, audioList.Length)], transform.position);
    }
}
