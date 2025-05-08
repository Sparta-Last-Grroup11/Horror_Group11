using UnityEngine;

public class MainObserverTrigger : MonoBehaviour
{
    private bool isPlayerOn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null && isPlayerOn == false)
        {
            isPlayerOn = true;
            StageTriggerController.Instance.ActivateTriggers();
        }
    }

}
