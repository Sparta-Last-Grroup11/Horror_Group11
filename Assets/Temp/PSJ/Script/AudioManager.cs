using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Unity.VisualScripting;
using UnityEngine.Pool;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] SoundSource curBgm;

    ObjectPool<SoundSource> audioPool;
    [SerializeField] SoundSource audioSourcePrefab;

    [SerializeField] Transform tempRoot;
    [SerializeField] Transform bgmRoot;

    protected override void Awake()
    {
        base.Awake();
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
    }

    void OnReturnedToPool(SoundSource obj)
    {
        obj.gameObject.SetActive(false);
    }

    void OnDestroyPoolObject(SoundSource obj)
    {
        Destroy(obj.gameObject);
    }

    #endregion
    public void Audio2DPlay(AudioClip clip, float volume = 1f)
    {
        //게임 오브젝트를 만든 후에 오디오 소스를 붙여서 clip을 재생
        if (clip == null)
        {
            Debug.LogWarning("재생할 오디오 클립이 없습니다.");
            return;
        }

        SoundSource obj = audioPool.Get();
        obj.transform.SetParent(tempRoot);

        AudioSource source = obj.GetComponent<AudioSource>();
        source.clip = clip;
        source.volume = Mathf.Clamp01(volume);
        source.spatialBlend = 0f;
        source.Play();

        // clip의 길이만큼 지난 후 오브젝트 파괴
        obj.GetComponent<SoundSource>().Play(clip.length);
    }

    public AudioSource Audio3DPlay(AudioClip clip, Vector3 pos, float volume = 1f, bool isLoop = false)
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
        source.spatialBlend = 1f;
        source.Play();

        if (!isLoop) 
            obj.Play(clip.length);
        return source;
    }

    public void AudioBGMPlay(AudioClip clip, bool isLoop = true)
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
        source.loop = isLoop;
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
}
