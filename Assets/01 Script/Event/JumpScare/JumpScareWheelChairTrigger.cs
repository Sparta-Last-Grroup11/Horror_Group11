using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareWheelChairTrigger : MonoBehaviour
{
    [SerializeField] private Rigidbody wheelchairRb;
    [SerializeField] private float force = 300f;
    [SerializeField] private AudioClip scareSound;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player")) return;

        triggered = true;

        wheelchairRb.isKinematic = false;

        float forwardPower = 3f;
        float upwardPower = 1.5f;
        Vector3 pushDir = transform.forward * forwardPower + Vector3.up * upwardPower;
        wheelchairRb.AddForce(pushDir, ForceMode.Impulse);

        Vector3 torque = transform.right * 3f; // y축이 아닌, 옆으로 뒤집히는 토크
        wheelchairRb.AddTorque(torque, ForceMode.Impulse);

        AudioManager.Instance.Audio3DPlay(scareSound, transform.position, 1.5f);
    }
}
