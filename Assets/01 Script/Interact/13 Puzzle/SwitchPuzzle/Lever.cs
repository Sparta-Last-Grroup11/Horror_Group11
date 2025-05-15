using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] protected bool isOn = false;
    [SerializeField] protected float leverDegree = -60f;
    [SerializeField] protected float duration = 0.5f;
    protected Quaternion onRotation;
    protected Quaternion offRotation;
    protected bool isAct;
    protected virtual void Awake()
    {
        isAct = false;
        offRotation = transform.rotation;
        onRotation = offRotation * Quaternion.Euler(leverDegree, 0, 0);

    }
   
    protected IEnumerator Movelever(Quaternion start, Quaternion end) //레버 작동
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
            isAct = false;
        }
    }
}
