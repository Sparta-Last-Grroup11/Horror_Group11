using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OpenableObject : MonoBehaviour, I_Openable
{
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;
     public virtual void Open()
    {
        AudioManager.Instance.Audio3DPlay(openClip, transform.position);
        StartCoroutine(OpenRoutine());
    }
    public virtual void Close()
    {
        AudioManager.Instance.Audio3DPlay(closeClip, transform.position);
        StartCoroutine(CloseRoutine());
    }

    protected abstract IEnumerator OpenRoutine();
    protected abstract IEnumerator CloseRoutine();
}
