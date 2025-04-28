using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] private List<Receiver> receivers = new List<Receiver>();

    public List<Receiver> GetReceivers()
    {
        return receivers;
    }

    public void AddReceiver(Receiver receiver)
    {
        if (!receivers.Contains(receiver))
        {
            receivers.Add(receiver);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CharacterController>() != null)
        foreach(Receiver receiver in receivers)
        {
            receiver.ReceiveTrigger();
        }
        gameObject.SetActive(false);
    }
}
