using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestInfo
{
    public int id;
    public string content;
}

public class QuestManager : Singleton<QuestManager>
{
    [SerializeField] private TextAsset questAsset;
    [SerializeField] List<QuestInfo> dialogList;
    [SerializeField] string questLog = "Quest";
    [SerializeField] private QuestUI questUI;
    
    private Dictionary<int, ChangeInteract> changer = new Dictionary<int, ChangeInteract>();
    protected override bool dontDestroy => false;

    int questNum;

    protected override void Awake()
    {
        base.Awake();
        questAsset = ResourceManager.Instance.Load<TextAsset>(ResourceType.JsonData, questLog);

        var path = "Quest";
        var Dialog = JsonConvert.DeserializeObject<Dictionary<string, List<QuestInfo>>>(questAsset.text);

        questNum = 0;

        if (Dialog.TryGetValue(path, out var monologues))
        {
            dialogList = monologues;
        }
        else
        {
            dialogList = new List<QuestInfo>(); // 안전한 초기화
            Debug.LogWarning("퀘스트 대사가 로드되지 않았습니다.");
        }
    }

    public void PlayQuest()
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
        if (questNum > -1&& questNum < dialogList.Count && dialogList[questNum].id == num )
        {
            PlayQuest();
            QuestMonologue(num);
            if (changer.ContainsKey(num))
            {
                ChangeLayer(num);
            }
            questNum++;
        }
    }

    public void GetQuestUI(QuestUI ui)
    {
        if(questUI == null)
            questUI = ui;
    }

    public int GetCurrentQuest()
    {
        return questNum;
    }

    public void AddChanger(ChangeInteract obj, int questID)
    {
        if (!changer.ContainsKey(questID))
            changer.Add(questID,obj);
    }

    void ChangeLayer(int questID)
    {
        changer[questID].ChangeLayer();
    }


    void QuestMonologue(int questID)
    {
        switch (questID)
        {
            case 2:
                break;
            case 3:
                break;
            case 4:
                MonologueManager.Instance.DialogPlay(10);
                MonologueManager.Instance.DialogPlay(9);
                break;
            case 5:
                MonologueManager.Instance.DialogPlay(21);
                break;
            case 6:
                break;
            case 7:
                MonologueManager.Instance.DialogPlay(22);
                break;
            case 8:
                MonologueManager.Instance.DialogPlay(24);
                break;
            case 9:
                MonologueManager.Instance.DialogPlay(25);
                break;
            case 10:
                MonologueManager.Instance.DialogPlay(2);
                MonologueManager.Instance.DialogPlay(15);
                MonologueManager.Instance.DialogPlay(26);
                MonologueManager.Instance.DialogPlay(27);
                break;
            case 11:
                MonologueManager.Instance.DialogPlay(12);
                break;

        }
    }
}
