using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

//심장 박동을 구현하려했는데 파티클 시스템의 velocity로 제어하면 매번 박동이 이뤄질 때 마다 프레임의 변화 혹은 프레임 간의 타임렉 때문인지
//물리 연산의 특징때문인지는 몰라도 움직임이 불규칙하고 마지막 높낮이가 변화함.
//그래서 파티클 시스템의 Trail과 내장되어있는 최적화 시스템만 이용하고 파티클의 재생과 움직임은 직접 스크립트로 제어한다
//애니메이션 커브를 이용해 끝 높낮이만은 확실하게 맞추도록 제어.
public class HeartBeat : BaseUI
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private Gradient color;
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particle;
    private ParticleSystem.MainModule main;
    private float originalYMulti;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [Range(1, 2)]
    [SerializeField] private float beatHeightMultiplier;
    [SerializeField] private float beatTime;
    [Range (1, 2)]
    [SerializeField] private float beatTimeMultiplier;
    private float targetBeatTime;
    private float curTime = 0;
    [SerializeField] AudioClip beatSound;

    private void Awake()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        main = ps.main;
    }

    protected override void Start()
    {
        beatHeightMultiplier = 1;
        beatTimeMultiplier = 1;
        targetBeatTime = beatTime * (1 / beatTimeMultiplier);
        particle = new ParticleSystem.Particle[ps.main.maxParticles];
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(curTime >= targetBeatTime)
        {
            curTime = 0;
            main.startColor = GameManager.Instance.player.isChased ? Color.red : color.Evaluate(beatTimeMultiplier - 1);
            targetBeatTime = beatTime * (1 / beatTimeMultiplier);
            Shoot();
        }    
    }

    void LateUpdate()
    {
        int count = ps.GetParticles(particle);

        for (int i = 0; i < count; i++)
        {
            float t = (particle[i].startLifetime - particle[i].remainingLifetime) / particle[i].startLifetime;
            Vector3 pos = particle[i].position;

            pos.x = width * t;
            pos.y = curve.Evaluate(t) * height * beatHeightMultiplier;
            particle[i].position = pos;
        }

        ps.SetParticles(particle, count);
    }

    void Shoot()
    {
        Invoke(nameof(audioPlay), 0.1f);
        ps.Play();
    }

    void audioPlay()
    {
        AudioManager.Instance.Audio2DPlay(beatSound, GameManager.Instance.player.isChased ? beatTimeMultiplier - 1 + 1f : beatTimeMultiplier - 1 + 0.4f, false, EAudioType.SFX);
    }

    public void ChanbeatSpeed(float ratio)
    {
        beatTimeMultiplier = 1 + ratio;
        beatHeightMultiplier = beatTimeMultiplier;
        UIManager.Instance.Get<GlitchUI>().GlitchWithDistance(ratio);

    }
}
