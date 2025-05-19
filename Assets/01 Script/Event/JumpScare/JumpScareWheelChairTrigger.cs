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

        wheelchairRb.isKinematic = false; // 떨어지도록
        wheelchairRb.AddForce(transform.forward * force + Vector3.up * 50f);

        AudioManager.Instance.Audio3DPlay(scareSound, transform.position);
    }
}
