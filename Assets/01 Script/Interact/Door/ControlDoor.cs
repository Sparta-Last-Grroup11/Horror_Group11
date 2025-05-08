using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ControlDoor : OpenableObject
{
    public float duration = 1f;
    private Quaternion openRotation;
    private Quaternion closeRotation;
    [SerializeField] private float openDegree = 90;

    private void Awake()
    {
        SetRotation();
    }
    protected void SetRotation()
    {
        closeRotation = transform.rotation;
        openRotation = closeRotation * Quaternion.Euler(0, openDegree, 0); ;
    }
    protected override IEnumerator OpenRoutine() //문 회전
    {
        float elapsed = 0f;
        try
        {
            while (elapsed < duration)
            {
                transform.rotation = Quaternion.Slerp(closeRotation, openRotation, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
        finally
        {
            transform.rotation = openRotation;
        }
    }

    protected override IEnumerator CloseRoutine() //문 회전
    {
        float elapsed = 0f;
        try
        {
            while (elapsed < duration)
            {
                transform.rotation = Quaternion.Slerp(openRotation, closeRotation, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
        finally
        {
            transform.rotation = closeRotation;
        }
    }
}
