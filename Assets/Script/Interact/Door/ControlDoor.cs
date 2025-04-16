using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ControlDoor : MonoBehaviour
{
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;
    public float duration = 1f;
    public void OpenTheDoor()
    {
        StartCoroutine(OpenDoorMotion());
    }


    IEnumerator OpenDoorMotion() //문 회전
    {
        AudioManager.Instance.Audio3DPlay(openClip, transform.position);
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


    public void CloseTheDoor()
    {
        StartCoroutine(CloseDoorMotion());
    }


    IEnumerator CloseDoorMotion() //문 회전
    {
        AudioManager.Instance.Audio3DPlay(closeClip, transform.position);
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 90, 0);

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
