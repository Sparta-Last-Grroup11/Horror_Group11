using UnityEngine;

public class DoorTriggerActivator : MonoBehaviour
{
    private bool isPlayerOn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null && isPlayerOn == false)
        {
            isPlayerOn = true;
            StageTriggerController.Instance.ActivateTriggers();
        }
    }

}
