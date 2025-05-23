using UnityEngine;

public class JumpScareTriggerReceiver : MonoBehaviour
{
    public TriggerForEvent eventTrigger;

    private void Awake()
    {
        if (eventTrigger == null)
            eventTrigger = GetComponentInParent<TriggerForEvent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpScare"))
        {
            var jumpScareReceiver = other.GetComponent<JumpScareReceiver>();
            if (jumpScareReceiver != null)
            {
                eventTrigger.AddReceiver(jumpScareReceiver);
                jumpScareReceiver.SetEventTrigger(eventTrigger);
            }
        }
    }
}
