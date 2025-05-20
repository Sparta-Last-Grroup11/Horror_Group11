using System.Collections.Generic;
using UnityEngine;
using static Extension;
using Newtonsoft.Json;
using Unity.AI.Navigation;

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
    public int triggerindex;
}

[System.Serializable]
public class TriggerRoot
{
    public List<TriggerInfo> StageTriggerInfo;
}

[System.Serializable]
public class ZombieTriggerPair
{
    public GameObject zombie;
    public GameObject trigger;

    public ZombieTriggerPair(GameObject zombie, GameObject trigger)
    {
        this.zombie = zombie;
        this.trigger = trigger;
    }
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
    private List<GameObject> objectJumpScares = new List<GameObject>();

    protected override bool dontDestroy => false;

    private NavMeshSurface surface;

    public List<(GameObject zombie, GameObject trigger)> zombieTriggerPairs = new();

    protected override void Awake()
    {
        base.Awake();
        typeNames = new Dictionary<string, GameObject>();
        surface = Instantiate(ResourceManager.Instance.Load<GameObject>(ResourceType.Enemy, "NavMeshSurface")).GetComponent<NavMeshSurface>();

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
        ZombieJumpScareMake();
        ObjectJumpScareMake();
    }

    private void PuzzleItemMake()
    {
        List<int> selects = RandomUniqueIndices(0, spawnRoot.spawnPoints["PuzzleSpawnPoint"].Count - 1, 5);
        for (int i = 1; i < 6; i++)
        {
            string path = "Offering_" + i.ToString();
            var obj = ResourceManager.Instance.Load<GameObject>(ResourceType.Item, path);
            Instantiate(obj, spawnRoot.spawnPoints["PuzzleSpawnPoint"][selects[i - 1]].position, Quaternion.identity, typeNames["Item"].transform).name = $"Offering_{i}";
        }
        Debug.Log("PuzzleItemMake");
    }

    private void ZombieJumpScareMake()
    {
        Debug.Log("[StageManager] TriggerMake() 호출됨");

        string path = "StageTriggerInfo";
        triggerAsset = ResourceManager.Instance.Load<TextAsset>(ResourceType.JsonData, path);

        if (triggerAsset == null)
        {
            Debug.Log("[StageManager]: TextAsset Loading Failed");
            return;
        }

        var triggerRoot = JsonConvert.DeserializeObject<TriggerRoot>(triggerAsset.text);
        var triggers = triggerRoot.StageTriggerInfo;

        var activePairs = new List<ZombieTriggerPair>();

        foreach (var trigger in triggers)
        {
            if (trigger.stageid == StageNum.StageNumber)
            {
                List<int> selects = RandomUniqueIndices(0, spawnRoot.spawnPoints["JumpScare_ZombieSpawnPoint"].Count - 1, trigger.triggerindex);

                for (int i = 1; i < trigger.triggerindex + 1; i++)
                {
                    var zombiePrefab = ResourceManager.Instance.Load<GameObject>(ResourceType.Event, "JumpScareZombie");
                    var spawnPos = spawnRoot.spawnPoints["JumpScare_ZombieSpawnPoint"][selects[i - 1]].position; 
                    var zombieInstance = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
                    zombieInstance.SetActive(false);

                    // 트리거존과 페어
                    var triggerObj = StageTriggerController.Instance.triggers[selects[i - 1]];
                    triggerObj.SetActive(false);
                    var pair = new ZombieTriggerPair(zombieInstance, triggerObj);
                    activePairs.Add(pair);
                }
            }
        }
        StageTriggerController.Instance.ReceivePairs(activePairs);
    }

    private void ObjectJumpScareMake()
    {
        string path = "JumpScareObjectTriggerGroup";
        var obj = ResourceManager.Instance.Load<GameObject>(ResourceType.Event, path);
        Instantiate(obj);
        Debug.Log("생성됨");

        objectJumpScares.Clear(); // 점프 스퀘어 리스트 초기화

        // 점프 스퀘어들 리스트업
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject child = obj.transform.GetChild(i).gameObject;
            child.SetActive(false);
            objectJumpScares.Add(child);
        }

        // 2개 만 골라서 활성화
        int activateCount = Mathf.Min(2, objectJumpScares.Count); // 2개 또는 리스트 개수 중 작은 값
        List<int> randomIndexes = new List<int>();

        while (randomIndexes.Count < activateCount)
        {
            int randomIndex = Random.Range(0, objectJumpScares.Count);

            if (!randomIndexes.Contains(randomIndex))
            {
                randomIndexes.Add(randomIndex);
                objectJumpScares[randomIndex].SetActive(true);
            }
        }
    }

}
