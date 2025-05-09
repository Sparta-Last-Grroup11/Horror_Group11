using UnityEngine;

public class JumpScaleZombieTriggerReceiver : MonoBehaviour
{
    public TriggerForEvent eventTrigger;

    private void Awake()
    {
        if (eventTrigger == null)
            eventTrigger = GetComponentInParent<TriggerForEvent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SkinLess"))
        {
            var jumpScaleReceiver = other.GetComponent<JumpScaleReceiver>();
            if (jumpScaleReceiver != null)
            {
                eventTrigger.AddReceiver(jumpScaleReceiver);
                jumpScaleReceiver.SetEventTrigger(eventTrigger);
            }
        }
    }

}
