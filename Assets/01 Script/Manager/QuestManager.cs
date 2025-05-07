using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    [SerializeField] private TextAsset questAsset;
    [SerializeField] List<MonologueInfo> dialogList;
    [SerializeField] string questLog = "QuestLog";
    [SerializeField] private QuestUI questUI;
    //GameObject dialogPrefab;

    protected override bool dontDestroy => false;
    int questNum;

    protected override void Awake()
    {
        base.Awake();
        //questAsset = ResourceManager.Instance.Load<TextAsset>(ResourceType.JsonData, questLog);
        questAsset = ResourceManager.Instance.Load<TextAsset>(ResourceType.JsonData, "Monologue");

        var path = "Quest";
        var Dialog = JsonConvert.DeserializeObject<Dictionary<string, List<MonologueInfo>>>(questAsset.text);

        questNum = 0;

        if (Dialog.TryGetValue(path, out var monologues))
        {
            dialogList = monologues;
        }
        else
        {
            dialogList = new List<MonologueInfo>(); // 안전한 초기화
            Debug.LogWarning("퀘스트 대사가 로드되지 않았습니다.");
        }
        QuestTrigger(questNum);
    }

    public void PlayQueue()
    {
        if (questUI == null)
        {
            UIManager.Instance.show<QuestUI>().ChangeQuest(dialogList[questNum].content);
        }
        else
        {
            questUI.ChangeQuest(dialogList[questNum].content);
        }
    }

    public void QuestTrigger(int num)
    {
        if (questNum == num && questNum < dialogList.Count)
        {
            PlayQueue();
            questNum++;
        }
    }
}
