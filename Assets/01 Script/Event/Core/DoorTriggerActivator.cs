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

            GameManager.Instance.CheckPointSave(new Vector3(-1.43f, 1.88f, -0.30f));
            GameManager.Instance.SetNursePhase(NurseZombie.SpawnNursePhase.FirstFloor);

        }
    }

}
