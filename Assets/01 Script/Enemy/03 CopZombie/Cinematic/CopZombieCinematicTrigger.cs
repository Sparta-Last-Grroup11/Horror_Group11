using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopZombieCinematicTrigger : MonoBehaviour
{
    public GameObject copzombieCinematic;
    public GameObject copZombiePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            copzombieCinematic.SetActive(true);
            GameManager.Instance.player.cantMove = true;
            Invoke("CinematicEnd", 5f);
        }
    }

    public void CinematicEnd()
    {
        copzombieCinematic.SetActive(false);
        GameManager.Instance.player.cantMove = false;
        Instantiate(copZombiePrefab, transform.position, Quaternion.Euler(0, 180, 0));
        Destroy(gameObject);
    }
}
