using System.Collections;
using Cinemachine;
using UnityEngine;

public class JumpScareZombie : Enemy, IJumpScareEvent
{
    [Header("Components")]
    public Animator jumpScareZombieAnim;
    public Rigidbody _rigidbody;
    public Transform cameraTransform;
    public AudioClip firstFoundSoundClip;
    public AudioClip chaseSoundClip;
    [SerializeField] private CinemachineVirtualCamera jumpScareCam;

    [Header("Movement")]
    public float rushSpeed = 10f;
    public float disappearTime = 5f;
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
        LookAtPlayer();
        jumpScareZombieAnim.SetTrigger("Idle");
        GameManager.Instance.player.isChased = true;
        FirstVisible(ref hasBeenSeenByPlayer, firstMonologueNum);

        StartCoroutine(RushToPlayer());
    }

    private IEnumerator RushToPlayer()
    {
        if (rushDelay > 0f)
        {
            yield return new WaitForSeconds(rushDelay);
        }

        bool introPlayed = false;
        float elapsed = 0f;

        while (elapsed < disappearTime)
        {
            if (!introPlayed)
            {
                jumpScareCam.Priority = 12;
                GameManager.Instance.player.cantMove = true;

                UIManager.Instance.GlitchStart(10f);
                AudioManager.Instance.Audio2DPlay(chaseSoundClip, 1f);
                jumpScareZombieAnim.SetTrigger("Chase");

                yield return new WaitForSeconds(0.8f);

                introPlayed = true;
            }

            LookAtPlayer();

            Vector3 target = PlayerTransform.position;
            target.y = transform.position.y;

            Vector3 direction = (target - transform.position).normalized; 
            _rigidbody.MovePosition(transform.position + direction * rushSpeed * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, target);
            if (distance < 0.3f) break;

            elapsed += Time.deltaTime;
            yield return null;
        }

        ZombieDisappear();
    }

    private void ZombieDisappear()
    {
        UIManager.Instance.GlitchEnd();
        GameManager.Instance.player.isChased = false;
        GameManager.Instance.player.cantMove = false;
        jumpScareCam.Priority = 8;
        Destroy(gameObject);
    }

}
