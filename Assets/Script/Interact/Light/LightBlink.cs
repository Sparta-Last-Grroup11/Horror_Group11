using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : Receiver
{
    [Header("LightList")]
    [SerializeField] private HashSet<Light> lights;
    [SerializeField] private float intensity;
    
    

    [Header("BlinkRange")]
    public Vector3 boxSize;
    private Vector3 boxOffset = Vector3.zero;

    [Header("BlinkSetting")]
    public float blinkTime = 0.3f;
    public float blinkCount = 2;

    private void Start()
    {
        lights = new HashSet<Light>();
    }
    public override void ReceiveTrigger()
    {
        if (!PuzzleManager.Instance.GetIsPowerOn()) return;
        lights.Clear();

        Vector3 center = transform.position + transform.rotation * boxOffset;
        Collider[] colliders = Physics.OverlapBox(center, boxSize * 0.5f, transform.rotation);
        Light light = null;
        foreach (Collider col in colliders)
        {
            if (col.TryGetComponent<Light>(out light)&&light.intensity > 0)
            {
                lights.Add(light);
            }
        }
        if (lights.Count > 0)
            StartCoroutine(Blink());
    }

    IEnumerator Blink()
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
