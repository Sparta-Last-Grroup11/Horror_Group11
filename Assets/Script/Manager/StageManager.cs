using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using Newtonsoft;
using static Extension;
using Newtonsoft.Json;

[System.Serializable]
public class StageInfo
{
    public string type;
    public string prefabname;
    public float[] position;
    public float[] rotation;
    public string description;
    /*
    public int position;
    */
}

public class StageManager : Singleton<StageManager>
{
    [SerializeField] private TextAsset textAsset;

    public List<StageInfo> currentStage;
    protected override bool dontDestroy => false;

    protected override void Awake()
    {
        base.Awake();
    }

    public void StageMake()
    {
        //디버깅 코드
        StageNum.StageNumber = 1;
        //디버깅 코드
        string name = $"Stage{StageNum.StageNumber}";
        textAsset = ResourceManager.Instance.Load<TextAsset>(ResourceType.JsonData, name);
        var allStages = JsonConvert.DeserializeObject<Dictionary<string, List<StageInfo>>>(textAsset.text);

        if (allStages.TryGetValue(name, out var stageList))
        {
            currentStage = stageList;

            foreach (var info in currentStage)
            {
                Debug.Log($"Type: {info.type}, Prefab: {info.prefabname}, Pos: {string.Join(",", info.position)}");
                GameObject obj = ResourceManager.Instance.Load<GameObject>(StringToEnum<ResourceType>(info.type), info.prefabname);
                GameObject prefab = Instantiate(obj, info.position.FloatToVector3(), info.rotation.FloatToQuaternion());
                prefab.name = obj.name;
                if (info.type.Equals(ResourceType.Item.ToString()))
                {
                    prefab.TryGetComponent<ClipItem>(out ClipItem clip);
                    if (clip != null)
                        clip.Description = info.description;
                }
            }
        }
        else
        {
            Debug.LogWarning("Stage1 key not found in JSON");
        }
    }
}
