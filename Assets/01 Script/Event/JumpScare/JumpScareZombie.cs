using System.Collections;
using UnityEngine;

public class JumpScareZombie : Enemy, IJumpScareEvent
{
    [Header("Components")]
    public Animator jumpScareZombieAnim;
    public Rigidbody _rigidbody;
    public Transform cameraTransform;
    public AudioClip spottedRoarClip;
    public AudioClip rushFootstepsLoop;

    [Header("Movement")]
    public float rushSpeed = 10f;
    public float disappearTime = 1f;
    public float rushDelay = 0f;

    [Header("Monologue")]
    public bool hasBeenSeenByPlayer = false;
    public int firstMonologueNum = 0;

    private Enemy enemy;

    private void Awake()
    {
        jumpScareZombieAnim = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    public void TriggerEvent()
    {
        GameManager.Instance.player.isChased = true;
        UIManager.Instance.GlitchStart(10f);
        jumpScareZombieAnim.SetTrigger("Chase");
        FirstVisible(ref hasBeenSeenByPlayer, firstMonologueNum);
        AudioManager.Instance.Audio2DPlay(spottedRoarClip, 1f);
        AudioManager.Instance.Audio2DPlay(rushFootstepsLoop, 1f);

        StartCoroutine(RushToPlayer());
    }

    private IEnumerator RushToPlayer()
    {
        if (rushDelay > 0f)
        {
            yield return new WaitForSeconds(rushDelay);
        }

        float elapsed = 0f;
        while (elapsed < disappearTime)
        {
            LookAtPlayer();
            Vector3 target = PlayerTransform.position;
            Vector3 direction = (target - transform.position).normalized; 
            _rigidbody.MovePosition(transform.position + direction * rushSpeed * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, target);
            if (distance < 1.0f) break;

            elapsed += Time.deltaTime;
            yield return null;
        }

        ZombieDisappear();
    }

    private void ZombieDisappear()
    {
        UIManager.Instance.GlitchEnd();
        GameManager.Instance.player.isChased = false;
        Destroy(gameObject);
    }

}
