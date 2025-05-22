using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Unity.VisualScripting;
using UnityEngine.Pool;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
using UnityEngine.SceneManagement;

public enum EAudioType
{
    Master,
    BGM,
    SFX
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] SoundSource curBgm;

    ObjectPool<SoundSource> audioPool;
    [SerializeField] SoundSource audioSourcePrefab;

    [SerializeField] Transform tempRoot;
    [SerializeField] Transform bgmRoot;
    private List<SoundSource> activeSources;

    protected override void Awake()
    {
        base.Awake();
        activeSources = new List<SoundSource>();
        audioMixer = ResourceManager.Instance.Load<AudioMixer>(ResourceType.Sound, "BasicAudioMixer");
        // 풀 초기화
        audioPool = new ObjectPool<SoundSource>(
            CreatePooledItem,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject,
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 50
        );
        audioSourcePrefab = Resources.Load<GameObject>("Sound/SoundSource").GetComponent<SoundSource>();
        tempRoot = new GameObject("Temp").transform;
        tempRoot.SetParent(transform);

        bgmRoot = new GameObject("BGM").transform;
        bgmRoot.SetParent(transform);
    }

    private void Start()
    {
        LoadAllAudioSetting();
    }

    #region 풀링
    SoundSource CreatePooledItem()
    {
        SoundSource soundSource = Instantiate(audioSourcePrefab).GetComponent<SoundSource>();
        soundSource.Init(audioPool);
        return soundSource;
    }

    void OnTakeFromPool(SoundSource obj)
    {
        obj.gameObject.SetActive(true);
        activeSources.Add(obj);
    }

    void OnReturnedToPool(SoundSource obj)
    {
        obj.gameObject.SetActive(false);
        activeSources.Remove(obj);
    }

    void OnDestroyPoolObject(SoundSource obj)
    {
        Destroy(obj.gameObject);
    }

    #endregion
    public void Audio2DPlay(AudioClip clip, float volume = 1f, bool isLoop = false, EAudioType type = EAudioType.Master)
    {
        if (clip == null)
        {
            Debug.LogWarning("재생할 오디오 클립이 없습니다.");
            return;
        }

        SoundSource obj = audioPool.Get();
        obj.transform.SetParent(tempRoot);

        AudioSource source = obj.GetComponent<AudioSource>();
        source.loop = isLoop;
        source.clip = clip;
        source.volume = Mathf.Clamp01(volume);
        source.spatialBlend = 0f;
        SetOutputGroup(source, type);
        source.Play();

        // clip의 길이만큼 지난 후 오브젝트 파괴
        if (isLoop == false)
        {
            obj.GetComponent<SoundSource>().Play(clip.length);
        }
    }

    public AudioSource Audio3DPlay(AudioClip clip, Vector3 pos, float volume = 1f, bool isLoop = false, EAudioType type = EAudioType.Master)
    {
        if (clip == null)
        {
            Debug.LogWarning("재생할 오디오 클립이 없습니다.");
            return null;
        }

        SoundSource obj = audioPool.Get();
        obj.transform.position = pos;
        obj.transform.SetParent(tempRoot);

        AudioSource source = obj.GetComponent<AudioSource>();
        source.clip = clip;
        source.loop = isLoop;
        source.volume = Mathf.Clamp01(volume);
        SetOutputGroup(source, type);
        source.spatialBlend = 1f;
        source.Play();

        if (!isLoop) 
            obj.Play(clip.length);
        return source;
    }

    public void AudioBGMPlay(AudioClip clip, bool isLoop = true, float volume = 1f, EAudioType type = EAudioType.Master)
    {
        if (clip == null)
        {
            Debug.LogWarning("재생할 BGM 클립이 없습니다.");
            return;
        }

        if (curBgm != null)
        {
            audioPool.Release(curBgm);
            curBgm = null;
        }
        SoundSource obj = audioPool.Get();
        obj.transform.SetParent(bgmRoot);

        AudioSource source = obj.GetComponent<AudioSource>();
        source.clip = clip;
        source.spatialBlend = 0f;
        source.volume = volume;
        source.loop = isLoop;
        SetOutputGroup(source, type);
        source.Play();

        if (isLoop == true)
            curBgm = obj;
        else
            obj.Play(clip.length);
    }

    public void StopBGM()
    {
        if (curBgm != null)
        {
            audioPool.Release(curBgm);
            curBgm = null;
        }
    }

    public void StopAllSounds()
    {
        if (activeSources == null) return;

        foreach (var source in activeSources.ToArray())
        {
            audioPool.Release(source);
        }
    }

    void LoadAllAudioSetting()
    {
        foreach (EAudioType type in System.Enum.GetValues(typeof(EAudioType)))
        {
            if (PlayerPrefs.HasKey(type.ToString()))
            {
                float CurrentValue = PlayerPrefs.GetFloat(type.ToString());
                SetLevel(CurrentValue, type);
            }
            else
            {
                SetLevel(0.5f, type);
            }
        }
    }

    public void SetLevel(float value, EAudioType type)
    {
        float ChangeValue;
        if (value <= 0.001f)
            ChangeValue = -80;
        else
            ChangeValue = Mathf.Log10(value) * 20;
        audioMixer.SetFloat(type.ToString(), ChangeValue);
        SaveAudioSetting(value, type);
    }

    public void LoadAudioSetting(EAudioType type, Slider slider = null)
    {
        if (PlayerPrefs.HasKey(type.ToString()))
        {
            float CurrentValue = PlayerPrefs.GetFloat(type.ToString());
            if (slider != null)
            {
                slider.value = Mathf.Clamp(CurrentValue, slider.minValue, slider.maxValue);
                SetLevel(CurrentValue, type);
            }
        }
    }

    public void SaveAudioSetting(float value, EAudioType type)
    {
        PlayerPrefs.SetFloat(type.ToString(), value);
    }

    private void SetOutputGroup(AudioSource source, EAudioType type)
    {
        AudioMixerGroup[] groups = audioMixer.FindMatchingGroups(type.ToString());
        if (groups != null && groups.Length > 0)
            source.outputAudioMixerGroup = groups[0];
        else
            source.outputAudioMixerGroup = null;
    }
}
