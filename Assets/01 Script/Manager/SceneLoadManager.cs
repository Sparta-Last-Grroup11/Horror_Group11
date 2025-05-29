using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

//로딩창의 표기. 그리고 비동기 씬 로딩을 위한 씬 로드 매니저.
public class SceneLoadManager : Singleton<SceneLoadManager>
{
    public string NowSceneName = "";

    protected override void Awake()
    {
        base.Awake();

        NowSceneName = SceneManager.GetActiveScene().name;
    }

    public async Task ChangeScene(string SceneName, Action callback = null, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        AudioManager.Instance.StopAllSounds();
        LoadingUI loadingUI = UIManager.Instance.show<LoadingUI>();
        loadingUI.Init();

        await Task.Delay(1000);
        var op = SceneManager.LoadSceneAsync(SceneName, loadSceneMode);

        while (!op.isDone)
        {
            loadingUI.SetLoadingProgress(op.progress);
            await Task.Yield();
        }

        if (loadSceneMode == LoadSceneMode.Single)
            NowSceneName = SceneName;

        callback?.Invoke();
    }

    public async Task UnLoadScene(string SceneName, Action callback = null)
    {
        var op = SceneManager.UnloadSceneAsync(SceneName);

        while (!op.isDone)
        {
            await Task.Yield();
        }

        callback?.Invoke();
    }
}
