using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ControlDoor : OpenableObject
{
    public float duration = 1f;
    
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
    protected override IEnumerator MoveRoutine(Quaternion start, Quaternion end) //문 회전
    {
        float elapsed = 0f;
        try
        {
            while (elapsed < duration)
            {
                transform.rotation = Quaternion.Slerp(start, end, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
        finally
        {
            transform.rotation = end;
        }
    }
}
