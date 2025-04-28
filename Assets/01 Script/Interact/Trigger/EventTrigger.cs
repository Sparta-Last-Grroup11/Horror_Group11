using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] private List<Receiver> receivers = new List<Receiver>();

    private void Awake()
    {
        if (receivers.Count == 0)
        {
            // 자기 자식 중 Receiver 찾아서 자동 등록
            Receiver receiver = GetComponentInChildren<Receiver>();
            if (receiver != null)
            {
                receivers.Add(receiver);
            }
        }
    }

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
        gameObject.SetActive(false);
    }
}
