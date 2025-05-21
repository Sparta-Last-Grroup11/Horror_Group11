using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    private PlayerFlash playerFlash;
    [Range(0f, 120f)] public float flashBattery = 120.0f;

    private Light spotLight;
    [Header("Angle")]
    [SerializeField] private float minInnerSpotAngle = 15f;
    [SerializeField] private float maxInnerSpotAngle = 20f;
    [SerializeField] private float minSpotAngle = 55f;
    [SerializeField] private float maxSpotAngle = 60f;

    [Header("Lerp")]
    [SerializeField] private float angleLerpSpeed = 1f;

    // 깜빡임 관련
    private float flickerTimer = 0f;
    private float flickerInterval;

    [SerializeField] private int deadFlicker = 0;

    private void OnEnable()
    {
        spotLight = GetComponent<Light>();
        playerFlash = GetComponentInParent<PlayerFlash>();
        spotLight.enabled = false;
    }

    private void Update()
    {
        if (playerFlash.isFlash)
        {
            if (flashBattery > 0)
            {
                // 1. 울렁임 효과 (자연스러운 angle 변화)
                AnimateLightAngles();

                // 2. 깜빡임 (배터리 20 이하일 때만)
                if (flashBattery <= 20f)
                {
                    FlickerRandomly();
                }
                else
                {
                    spotLight.enabled = true;
                }
            }
            else
            {
                if (deadFlicker == 0)
                {
                    DeadFlicker();
                }
            }

            flashBattery -= Time.deltaTime * 1f;
            flashBattery = Mathf.Clamp(flashBattery, 0f, 120f);
        }
    }

    private void AnimateLightAngles()
    {
        float targetInnerAngle = Mathf.Lerp(minInnerSpotAngle, maxInnerSpotAngle, Mathf.PingPong(Time.time * angleLerpSpeed, 1f));
        float targetSpotAngle = Mathf.Lerp(minSpotAngle, maxSpotAngle, Mathf.PingPong(Time.time * angleLerpSpeed, 1f));

        spotLight.innerSpotAngle = targetInnerAngle;
        spotLight.spotAngle = targetSpotAngle;
    }

    private void FlickerRandomly()
    {
        flickerTimer += Time.deltaTime;
        flickerInterval = Random.Range(4f, 6f);

        if (flickerTimer >= flickerInterval)
        {
            flickerTimer = 0f;
            flickerInterval = Random.Range(3f, 6f); // 깜빡이는 간격 랜덤
            StartCoroutine(Flicker(null, 0.1f));
        }
    }

    private void DeadFlicker()
    {
        deadFlicker++;
        StartCoroutine(Flicker(() =>
        {
            spotLight.enabled = false;
        }, 0.5f));
        playerFlash.isFlash = false;
    }

    private IEnumerator Flicker(System.Action onFinish, float flickerTime)
    {
        spotLight.enabled = false;
        yield return new WaitForSeconds(flickerTime);
        spotLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        spotLight.enabled = false;
        yield return new WaitForSeconds(flickerTime);
        spotLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        onFinish?.Invoke();
    }

    public void RechargeBattery()
    {
        deadFlicker = 0;
        flashBattery = 120;
    }
}
