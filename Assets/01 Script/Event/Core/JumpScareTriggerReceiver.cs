using UnityEngine;

// 점프스케어 좀비와 트리거존을 연결하여 이벤트 전달 역할을 수행
// 리소스 로드되는 점프스케어 좀비가 트리거존에 진입하면 이벤트 등록 및 전달세팅
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
