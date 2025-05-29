using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartSceneLight : MonoBehaviour
{
    //인스펙터에서 대부분을 다 설정할 수 있도록 함.
    [SerializeField] Light light;
    [SerializeField] float targetIntensity;
    private float curIntensity;
    [SerializeField] float intensitySpeed;
    [SerializeField] Color[] color;
    [SerializeField] int colorNumber;
    [SerializeField] bool isChanging;

    private void Awake()
    {
        isChanging = false;
    }

    private void Update()
    {
        Effect();
    }

    private void Effect()
    {
        if (isChanging == false)
        {
            Color changeColor = color[Random.Range(0, color.Length)];
            light.color = changeColor;
            StartCoroutine(LightEffectCoroutine());
        }
    }

    IEnumerator LightEffectCoroutine()
    {
        isChanging = true;
        light.intensity = 0;
        while(light.intensity < targetIntensity)
        {
            light.intensity += Time.deltaTime * intensitySpeed;
            yield return null;
        }
        light.intensity = targetIntensity;
        while (light.intensity > 0)
        {
            light.intensity -= Time.deltaTime * intensitySpeed;
            yield return null;
        }
        light.intensity = 0;
        isChanging = false;
        yield return null;
    }
}
