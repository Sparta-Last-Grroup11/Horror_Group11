using UnityEngine;

public class DoorTriggerActivator : MonoBehaviour
{
    private bool isPlayerOn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null && isPlayerOn == false)
        {
            Debug.LogWarning("문 트리거 발동");
            isPlayerOn = true;
            StageTriggerController.Instance.ActivateTriggers();
        }
    }

}
