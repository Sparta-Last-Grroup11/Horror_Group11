using System.Collections.Generic;
using UnityEngine;

public class TriggerForEvent : MonoBehaviour //이벤트용 트리거
{
    public List<Receiver> receivers = new List<Receiver>();
    [SerializeField] private GameObject endingCutScene;

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
        endingCutScene.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
