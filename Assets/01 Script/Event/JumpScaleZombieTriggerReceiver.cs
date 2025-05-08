using UnityEngine;

public class JumpScaleZombieTriggerReceiver : MonoBehaviour
{
    public EventTrigger eventTrigger;
    private bool isZombieOn;

    private void Awake()
    {
        if (eventTrigger == null)
            eventTrigger = GetComponentInParent<EventTrigger>();
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
            if (isZombieOn == false)
            {
                isZombieOn = true;
                jumpScaleReceiver.gameObject.SetActive(false);
                eventTrigger.transform.parent.gameObject.SetActive(false);
            }

        }
    }

}
