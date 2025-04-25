using System.Collections;
using Unity.Burst;
using UnityEngine;
using UnityEngine.UIElements;

public class Lamp : MonoBehaviour
{
    [SerializeField] private SwitchController switchController;
    [SerializeField] private int rand;
    [SerializeField] private Light[] lampLights;
    private Coroutine coroutine;
    private void Awake()
    {
        coroutine = null;
    }

    private void Start() //switch의 list에 해당 오브젝트 저장
    {
        if (switchController != null)
        {
            switchController.AddLight(this);
        }
        TurnOff();
    }

    public void SetLight(float inten, float range, int ran) //light 설정
    {
        foreach (Light light in lampLights)
        {
            light.intensity = inten;
            light.range = range;
            rand = ran;
        }
    }

    public void TurnOn() //light 활성화 및 blink 실행
    {
        if (lampLights != null)
        {
            foreach(Light light in lampLights)
                light.enabled = true;
        }
        if(coroutine == null)
            coroutine = StartCoroutine(Blink());
    }

    public void TurnOff() //light 비활성화 및 blink 종료
    {
        if (lampLights != null)
        {
            foreach (Light light in lampLights)
                light.enabled = false;
        }
        if (coroutine != null)
            StopCoroutine(coroutine);
    }

    public IEnumerator Blink() //일정 시간마다 일정 확률로 불 꺼짐
    {
        WaitForSeconds delay = new WaitForSeconds(5);
        WaitForSeconds blinkTime = new WaitForSeconds(0.2f);
        while (true)
        {
            if (Random.Range(0, rand) == 1)
            {
                foreach (Light light in lampLights)
                    light.enabled = false;
                yield return blinkTime;

                foreach (Light light in lampLights)
                    light.enabled = true;
                yield return blinkTime;
                foreach (Light light in lampLights)
                    light.enabled = false;

                yield return new WaitForSeconds(Random.Range(1f, 5f));

                foreach (Light light in lampLights)
                    light.enabled = true;
                yield return blinkTime;
                foreach (Light light in lampLights)
                    light.enabled = false;

                foreach (Light light in lampLights)
                    light.enabled = true;
            }
            yield return delay;
        }
    }
}
