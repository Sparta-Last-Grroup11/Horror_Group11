using System.Collections;
using UnityEngine;

public class JumpScaleZombie : Enemy   // 점프스케어 기믹 (플레이어 보면 빠르게 달려와서 깜놀시키고 사라짐, 무해함)
{
    [Header("Components")]
    public Animator skinLessZombieAnim;
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

    protected override void Awake()
    {
        skinLessZombieAnim = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void TriggerEventEnemy()
    {
        TriggerAmbush();
    }

    private void TriggerAmbush()
    {
        GameManager.Instance.player.isChased = true;
        UIManager.Instance.GlitchStart(10f);
        skinLessZombieAnim.SetTrigger("Chase");
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
        while (elapsed < disappearTime)  // 최대 추적 시간 동안만 반복
        {
            LookAtPlayer();
            Vector3 target = PlayerTransform.position;
            Vector3 direction = (target - transform.position).normalized; 
            _rigidbody.MovePosition(transform.position + direction * rushSpeed * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, target);
            if (distance < 1.0f) break;  // 플레이어와 1.0f 이하 거리로 가까워지면 즉시 break

            elapsed += Time.deltaTime;
            yield return null;
        }

        UIManager.Instance.GlitchEnd();
        GameManager.Instance.player.isChased = false;
        Destroy(gameObject);
    }

}
