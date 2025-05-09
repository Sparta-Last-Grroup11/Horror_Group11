public class JumpScareReceiver : Receiver
{
    private IJumpScareEvent scareEvent;

    protected override void Awake()
    {
        base.Awake();
        scareEvent = GetComponent<IJumpScareEvent>();
    }

    public override void ReceiveTrigger()
    {
        scareEvent?.TriggerEvent();
    }


    public void SetEventTrigger(TriggerForEvent trigger)
    {
        eventTrigger = trigger;
        eventTrigger.AddReceiver(this);
    }

}
