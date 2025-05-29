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

//스테이지 생성 시에 스폰되어야 하는 아이템들, 적, 퍼즐 등등을 스폰시킴.
//구글 스프레드 시트를 Json 파일로 변환해 해당 Json파일을 읽어옴
//상단의 StageInfo클래스가 Json파일에 표기되어있는 변수들을 저장시킬 클래스다.
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

                if (info.prefabname.Equals("NurseZombie"))
                {
                    GameManager.Instance.nurse = prefab.GetComponentInChildren<Enemy>();
                }
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
        // 트리거 개수(triggerindex) 정보를 담은 Json 파일 로드
        string path = "StageTriggerInfo";
        triggerAsset = ResourceManager.Instance.Load<TextAsset>(ResourceType.JsonData, path);

        if (triggerAsset == null)
        {
            return;
        }

        var triggerRoot = JsonConvert.DeserializeObject<TriggerRoot>(triggerAsset.text);
        var triggers = triggerRoot.StageTriggerInfo;

        // 현재 스테이지에 해당하는 트리거 개수(triggerindex) 가져옴
        int? triggerCount = null;
        foreach (var trigger in triggers)
        {
            if (trigger.stageid == StageNum.StageNumber)
            {
                triggerCount = trigger.triggerindex;
                break;
            }
        }

        if (triggerCount == null) return;

        var allPairs = new List<ZombieTriggerPair>();
        var spawnPoints = spawnRoot.spawnPoints["JumpScare_ZombieSpawnPoint"];
        var triggerObjs = StageTriggerController.Instance.triggers;

        int count = Mathf.Min(spawnPoints.Count, triggerObjs.Count);

        for (int i = 0; i < count; i++)
        {
            // 점프스케어 좀비 프리팹 인스턴스화 및 위치 설정
            var zombiePrefab = ResourceManager.Instance.Load<GameObject>(ResourceType.Event, "JumpScareZombie");
            var spawnPos = spawnPoints[i].position;
            var spawnRot = spawnPoints[i].rotation;

            var zombieInstance = Instantiate(zombiePrefab, spawnPos, spawnRot);
            zombieInstance.SetActive(true);

            // 트리거 오브젝트 비활성화 후 짝지음
            var triggerObj = triggerObjs[i];
            triggerObj.SetActive(false);

            allPairs.Add(new ZombieTriggerPair(zombieInstance, triggerObj));
        }

        // 트리거 개수만큼 무작위로 점브스케어 좀비-트리거 쌍 선택
        int activeCount = Mathf.Min(triggerCount.Value, allPairs.Count);
        List<int> selectedIndices = RandomUniqueIndices(0, allPairs.Count - 1, activeCount);

        var activePairs = new List<ZombieTriggerPair>();
        foreach(int index in selectedIndices)
        {
            var pair = allPairs[index];
            activePairs.Add(pair);
        }

        // 선택된 쌍을 전달 (이후 활성화됨)
        StageTriggerController.Instance.ReceivePairs(activePairs);
    }

    private void ObjectJumpScareMake()
    {
        string path = "JumpScareObjectTriggerGroup";
        var obj = ResourceManager.Instance.Load<GameObject>(ResourceType.Event, path);
        var groupInstance = Instantiate(obj);
        Debug.Log("생성됨");

        objectJumpScares.Clear(); // 점프 스퀘어 리스트 초기화

        // 점프 스퀘어들 리스트업
        for (int i = 0; i < groupInstance.transform.childCount; i++)
        {
            GameObject child = groupInstance.transform.GetChild(i).gameObject;
            child.SetActive(false);
            objectJumpScares.Add(child);
        }

        // 2개 만 골라서 활성화
        int activateCount = Mathf.Min(2, objectJumpScares.Count); // 2개 또는 리스트 개수 중 작은 값
        List<int> randomIndexes = RandomUniqueIndices(0, objectJumpScares.Count - 1, 2);

        foreach (int index in randomIndexes)
        {
            objectJumpScares[index].SetActive(true);
        }
    }
}
