using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

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
            main.startColor = color.Evaluate(beatTimeMultiplier - 1);
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
        AudioManager.Instance.Audio2DPlay(beatSound, beatTimeMultiplier - 1 + 0.1f);
    }

    public void ChanbeatSpeed(float ratio)
    {
        beatTimeMultiplier = 1 + ratio;
        beatHeightMultiplier = beatTimeMultiplier;
        UIManager.Instance.Get<GlitchUI>().GlitchWithDistance(ratio);

    }
}
