using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSafeDoor : MonoBehaviour
{
    [SerializeField] private AudioSource aduioSource;

    public float duration = 1f;
    public void OpenDoor()
    {
        StartCoroutine(RotateY90());
    }


    IEnumerator RotateY90()
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

        // 마지막에 정확하게 마무리
        transform.rotation = endRotation;
    }
}
