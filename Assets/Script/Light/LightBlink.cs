using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightBlink : Receiver
{
    [Header("LightList")]
    [SerializeField] private HashSet<Light> lights = new HashSet<Light>();
    [SerializeField] private float intensity;
    
    private Light light = null;

    [Header("BlinkRange")]
    public Vector3 boxSize;
    private Vector3 boxOffset = Vector3.zero;

    [Header("BlinkSetting")]
    public float blinkTime = 0.3f;
    public float blinkCount = 2;

    public override void ReceiveTrigger()
    {
        lights.Clear();

        Vector3 center = transform.position + transform.rotation * boxOffset;
        Collider[] colliders = Physics.OverlapBox(center, boxSize * 0.5f, transform.rotation);
        foreach (Collider col in colliders)
        {
            if (col.TryGetComponent<Light>(out light))
            {
                lights.Add(light);
            }
        }
        if (lights.Count > 0)
            StartCoroutine(blink());
    }

    IEnumerator blink()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            LightsBlink();
            yield return new WaitForSeconds(blinkTime);
            LightsBlink(intensity);
            yield return new WaitForSeconds(blinkTime);
        } 
    }

    void LightsBlink(float intens = 0)
    {
        foreach (Light light in lights)
        {
            if(light != null)
                light.intensity = intens;
        }
    }
}
