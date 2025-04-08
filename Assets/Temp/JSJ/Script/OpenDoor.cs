using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private AudioSource aduioSource;

    public float duration = 1f;
    public void OpenTheDoor()
    {
        StartCoroutine(OpenDoorMotion());
    }


    IEnumerator OpenDoorMotion() //문 회전
    {
        aduioSource.Play();
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, -90, 0);

        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
    }
}
