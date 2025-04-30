using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public List<Receiver> receivers = new List<Receiver>();

    public void AddReceiver(Receiver receiver)
    {
        if (receiver !=  null && !receivers.Contains(receiver))
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
    }
}
