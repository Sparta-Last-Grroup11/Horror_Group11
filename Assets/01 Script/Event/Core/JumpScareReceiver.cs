using UnityEngine;

public class JumpScareReceiver : Receiver  // JumpScareZombie 전용 트리거 수신기 (이벤트 발동 전용)
{
    [SerializeField] private JumpScareZombie jumpScareZombie;

    protected override void Awake()
    {
        base.Awake();
        jumpScareZombie = GetComponent<JumpScareZombie>();
    }

    public override void ReceiveTrigger()
    {
        jumpScareZombie?.TriggerEvent();
    }

    public void SetEventTrigger(TriggerForEvent trigger)
    {
        eventTrigger = trigger;
        eventTrigger.AddReceiver(this);
    }
}
