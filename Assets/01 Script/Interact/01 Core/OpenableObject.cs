using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OpenableObject : MonoBehaviour, I_Openable
{
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;
    protected Quaternion openRotation;
    protected Quaternion closeRotation;
    public virtual void Open() //열기용 Coroutine 실행 + 소리 재생
    {
        if (openClip != null)
            AudioManager.Instance.Audio3DPlay(openClip, transform.position);
        StartCoroutine(MoveRoutine(closeRotation, openRotation));
    }
    public virtual void Close()//닫기용 Coroutine 실행 + 소리 재생
    {
        if (closeClip != null)
            AudioManager.Instance.Audio3DPlay(closeClip, transform.position);
        StartCoroutine(MoveRoutine(openRotation, closeRotation));
    }

    protected abstract IEnumerator MoveRoutine(Quaternion start, Quaternion end);
}
