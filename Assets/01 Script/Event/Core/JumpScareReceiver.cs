using UnityEngine;

public class JumpScareReceiver : Receiver
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
