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
    public float rushSpeed = 30f;         // 달려드는 속도
    public float disappearTime = 1f;    // 사라지기까지 시간
    public float rushDelay = 0f; // 달려들기 전에 대기하는 시간
    public float timer = 0f;  // 달려든 후 일정 시간 지나면 사라지게 만들 타이머

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

        LookAtPlayer();
        MoveTowardPlayer(3.0f);
        ZombieDisappear();
    }

    private void MoveTowardPlayer(float verticalOffset)
    {
        Vector3 target = PlayerTransform.position + Vector3.up * verticalOffset;
        Vector3 direction = (target - transform.position).normalized;
        _rigidbody.MovePosition(transform.position + direction * rushSpeed * Time.deltaTime);
    }

    private void ZombieDisappear()
    {
        float distance = Vector3.Distance(transform.position, PlayerTransform.position);
        if (distance < 1.0f)
        {
            GameObject.Destroy(gameObject);
            UIManager.Instance.GlitchEnd();
            GameManager.Instance.player.isChased = false;
        }
    }
}
