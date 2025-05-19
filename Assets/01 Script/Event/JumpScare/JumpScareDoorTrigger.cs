using System.Collections;
using UnityEngine;

public class JumpScareDoorTrigger : MonoBehaviour
{
    [SerializeField] private ControlDoor door;
    [SerializeField] private float delayBeforeClose = 2.5f;
    [SerializeField] private AudioClip scareDoorSound;

    private bool doorIsTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (doorIsTriggered || !other.CompareTag("Player"))
            return;

        if (door is LockedDoor lockedDoor && lockedDoor.IsOpened)
        {
            if (Random.value <= 0.2f)
            {
                doorIsTriggered = true;
                StartCoroutine(CloseDoorAfterDelay());
            }
        }
    }

    private IEnumerator CloseDoorAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeClose);

        if (door is LockedDoor lockedDoor)
        {
            lockedDoor.OpenCloseDoor();
        }

        AudioManager.Instance.Audio2DPlay(scareDoorSound);
        
    }
}
