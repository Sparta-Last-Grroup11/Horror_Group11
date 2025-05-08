using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using Newtonsoft;
using static Extension;
using Newtonsoft.Json;
using Unity.AI.Navigation;
using System.Linq;

[System.Serializable]
public class StageInfo
{
    public string type;
    public string prefabname;
    public float[] position;
    public float[] rotation;
    public string description;
}

[System.Serializable]
public class TriggerInfo
{
    public int stageid;
    public int[] triggers;
}

public class StageManager : Singleton<StageManager>
{
    [Header("Spawn About")]
    [SerializeField] private TextAsset textAsset;
    public List<StageInfo> currentStage;
    Dictionary<string, GameObject> typeNames;
    public SpawnRoot spawnRoot;

    [Header("TriggerAbout")]
    [SerializeField] private TextAsset triggerAsset;
    public List<TriggerInfo> triggers;

    protected override bool dontDestroy => false;

    private NavMeshSurface surface;

    protected override void Awake()
    {
        base.Awake();
        typeNames = new Dictionary<string, GameObject>();
        surface = Instantiate(ResourceManager.Instance.Load<GameObject>(ResourceType.Enemy, "NavMeshSurface")).GetComponent<NavMeshSurface>();
    }

    public void SurfaceUpdate()
    {
        surface.BuildNavMesh();
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
                if (obj == null)
                    continue;
                GameObject prefab;
                if (!typeNames.ContainsKey(info.type))
                {
                    GameObject parent = new GameObject(info.type);
                    typeNames.Add(info.type, parent);
                }
                Quaternion rot;
                if (info.rotation == null)
                    rot = Quaternion.identity;
                else
                    rot = info.rotation.FloatToQuaternion();
                prefab = Instantiate(obj, info.position.FloatToVector3(), rot, typeNames[info.type].transform);
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
        PuzzleItemMake();
        TriggerMake();
        
    }

    public void TriggerMake()
    {
        Debug.Log("[StageManager] TriggerMake() 호출됨");

        string name = "StageTriggerInfo";
        triggerAsset = ResourceManager.Instance.Load<TextAsset>(ResourceType.JsonData, name);

        if (triggerAsset == null)
        {
            Debug.Log("[StageManager]: TextAsset Loading Failed");
            return;
        }

        var stageTrigger = JsonConvert.DeserializeObject<Dictionary<string, List<TriggerInfo>>>(triggerAsset.text);

        if(stageTrigger.TryGetValue(name, out var stageTriggerList))
        {
            triggers = stageTriggerList;
            foreach(var trigger in triggers)
            {
                Debug.Log(trigger);
                if (trigger.stageid == StageNum.StageNumber)
                {
                    StageTriggerController.Instance.GetTriggers(trigger.triggers.ToList()); 
                    Debug.Log(trigger.triggers);
                }
            }
        }
    }

    private void PuzzleItemMake()
    {
        List<int> selects = RandomUniqueIndices(0, spawnRoot.spawnPoints["PuzzleSpawnPoint"].Count - 1, 5);
        for (int i = 1; i < 6; i++)
        {
            string path = "Offering_" + i.ToString();
            var obj = ResourceManager.Instance.Load<GameObject>(ResourceType.Item, path);
            Instantiate(obj, spawnRoot.spawnPoints["PuzzleSpawnPoint"][selects[i - 1]].position, Quaternion.identity, typeNames["Item"].transform);
        }
    }
}
