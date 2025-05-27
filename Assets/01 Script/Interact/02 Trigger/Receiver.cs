using UnityEngine;

public abstract class Receiver : MonoBehaviour //리시버 기본 코드
{
    [SerializeField] protected TriggerForEvent eventTrigger;

    protected virtual void Awake()
    {
        if (eventTrigger != null)
        {
            eventTrigger.AddReceiver(this);
        }
        else
        {
            Debug.LogWarning($"[Receiver] {name}의 eventTrigger가 설정되지 않았습니다.");
        }
    }
    public abstract void ReceiveTrigger();
}
