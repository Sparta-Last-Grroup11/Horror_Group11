using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchUI : BaseUI
{
    Material glitchMat;
    [SerializeField] float maxGlitch = 50f;
    Coroutine curCourt;

    private void Awake()
    {
        glitchMat = ResourceManager.Instance.Load<Material>(ResourceType.Material, "ScreenGlitchShader");    
    }

    public void GlitchStartWithTime(float time, float amount = 50f)
    {
        if (curCourt != null)
            StopCoroutine(curCourt);
        curCourt = StartCoroutine(GlitchEffect(time, amount));
    }

    public void GlitchWithDistance(float ratio)
    {
        glitchMat.SetFloat("_NoiseAmount", maxGlitch * ratio);
        if (ratio < 0.33f)
        {
            glitchMat.SetFloat("_NoiseAmount", 0);
        }
        else
        {
            glitchMat.SetFloat("_GlitchStrength", 1);
        }
    }

    public void GlitchStart(float amount)
    {
        glitchMat.SetFloat("_NoiseAmount", amount);
        glitchMat.SetFloat("_GlitchStrength", 1);
    }

    public void GlitchEnd()
    {
        glitchMat.SetFloat("_NoiseAmount", 0);
        glitchMat.SetFloat("_GlitchStrength", 0);
    }

    IEnumerator GlitchEffect(float time, float amount)
    {
        glitchMat.SetFloat("_NoiseAmount", amount);
        glitchMat.SetFloat("_GlitchStrength", 1);

        yield return new WaitForSeconds(time);

        glitchMat.SetFloat("_NoiseAmount", 0);
        glitchMat.SetFloat("_GlitchStrength", 0);

        curCourt = null;
        yield return null;
    }

    protected override void OnDestroy()
    {
        GlitchEnd();
        base.OnDestroy();
    }
}
