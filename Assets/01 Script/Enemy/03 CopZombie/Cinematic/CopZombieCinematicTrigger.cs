using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopZombieCinematicTrigger : MonoBehaviour
{
    public GameObject copzombieCinematic;
    public GameObject copZombiePrefab;
    [SerializeField] private AudioClip meetCopZombieClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            copzombieCinematic.SetActive(true);
            GameManager.Instance.player.cantMove = true;
            AudioManager.Instance.Audio2DPlay(meetCopZombieClip, 1, false, EAudioType.SFX);
            StartCoroutine(CinematicEnd());
        }
    }

    IEnumerator CinematicEnd()
    {
        yield return new WaitForSeconds(5f);
        copzombieCinematic.SetActive(false);
        GameManager.Instance.player.cantMove = false;
        Instantiate(copZombiePrefab, copzombieCinematic.transform.position + new Vector3(-1, 0, 3), Quaternion.Euler(0, 150, 0));
        Destroy(gameObject);
    }
}
