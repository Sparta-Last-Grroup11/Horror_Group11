using System.Collections.Generic;

[System.Serializable]
public class TriggerGroupData
{
    public int stage;  // 몇 번째 스테이지인지
    public List<int> activeIndices;  // 켜질 Observer 인덱스들
}
