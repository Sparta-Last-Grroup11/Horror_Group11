using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class JumpScareThunder : MonoBehaviour
{
    private Volume globalVolume;
    private ColorAdjustments colorAdjustments;
    [SerializeField] private AudioClip thunderSoundClip;

    [SerializeField] private float thunderMinRate;
    [SerializeField] private float thunderMaxRate;
    private float nextThunderRate;
    private float afterLastThunderTime;

    private void Awake()
    {
        globalVolume = GetComponent<Volume>();
        globalVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
    }

    private void Start()
    {
        enabled = true;
        nextThunderRate = Random.Range(thunderMinRate, thunderMaxRate);
    }

    private void Update()
    {
        afterLastThunderTime += Time.deltaTime;
        if (afterLastThunderTime > nextThunderRate)
        {
            StartCoroutine(ThunderLighting());
            afterLastThunderTime = 0;
            nextThunderRate = Random.Range(thunderMinRate, thunderMaxRate);
        }
    }

    IEnumerator ThunderLighting()
    {
        AudioManager.Instance.Audio2DPlay(thunderSoundClip, 1, false, EAudioType.SFX);
        yield return null;
        colorAdjustments.postExposure.value = 5f;
        yield return new WaitForSeconds(0.08f);
        colorAdjustments.postExposure.value = 0.5f;
        yield return new WaitForSeconds(0.2f);
        colorAdjustments.postExposure.value = 5f;
        yield return new WaitForSeconds(0.2f);
        DOTween.To(() => colorAdjustments.postExposure.value, x => colorAdjustments.postExposure.value = x, 0.5f, 0.5f);
        yield return new WaitForSeconds(0.2f);
        colorAdjustments.postExposure.value = 5f;
        yield return new WaitForSeconds(0.1f);
        colorAdjustments.postExposure.value = 0.5f;
        yield return new WaitForSeconds(0.1f);
        colorAdjustments.postExposure.value = 3f;
        yield return new WaitForSeconds(0.1f);
        DOTween.To(() => colorAdjustments.postExposure.value, x => colorAdjustments.postExposure.value = x, 0f, 1);
    }
}
