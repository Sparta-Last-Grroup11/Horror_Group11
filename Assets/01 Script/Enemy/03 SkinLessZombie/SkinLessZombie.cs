using UnityEngine;

public class SkinLessZombie : Enemy   // 점프스케어 기믹 (플레이어 보면 빠르게 달려와서 깜놀시키고 사라짐, 무해함)
{
    [Header("Components")]
    public Animator skinLessZombieAnim;
    public Rigidbody rigidbody;
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

    private void Awake()
    {
        skinLessZombieAnim = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    protected override void Start()
    {
        base.Start();
        fsm = new EnemyStateMachine();
    }

    public override void TriggerEventEnemy()
    {
        TriggerAmbush();
    }

    public void TriggerAmbush()
    {
        fsm.ChangeState(new SkinLessZombieAmbushState(this, fsm));
    }

}
