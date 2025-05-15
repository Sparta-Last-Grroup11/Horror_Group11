using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OpenableObject : MonoBehaviour, I_Openable
{
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;
    protected Quaternion openRotation;
    protected Quaternion closeRotation;
    public virtual void Open()
    {
        if (openClip != null)
            AudioManager.Instance.Audio3DPlay(openClip, transform.position);
        StartCoroutine(MoveRoutine(closeRotation, openRotation));
    }
    public virtual void Close()
    {
        if (closeClip != null)
            AudioManager.Instance.Audio3DPlay(closeClip, transform.position);
        StartCoroutine(MoveRoutine(openRotation, closeRotation));
    }

    protected abstract IEnumerator MoveRoutine(Quaternion start, Quaternion end);
}
