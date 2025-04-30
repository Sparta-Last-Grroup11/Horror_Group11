using UnityEngine;
using UnityEngine.AI;

public class NurseZombie : Enemy   // 웃는 천사 기믹 (멈춰있다가, 플레이어가 뒤돌면 쫓아옴)
{
    // Components
    [HideInInspector] public NavMeshAgent nurseZombieAgent;
    public Animator nurseZombieAnim;
    public Rigidbody rb;
    public AudioClip nurseZombieChaseClip;
    public LightStateSO lightStateSO;

    // Movement & Attack
    public float moveSpeed = 2f;
    public float attackRange = 2f;
    public float dashSpeed = 6f;
    public float dashTriggerRange = 0.5f; // 공격 범위
    public bool hasDashed = false; // 돌진 완료 여부

    // Detection
    public float detectionRange = 10f;
    public bool wasLookedByPlayer = false;  // 지난 프레임에 나를 봤는지
    public bool isTriggeredByBackTurn = false;  // 봤다가 안 보는 순간에만 한 번 트리거됨
    public bool hasDetectedPlayer = false;
    public float PlayerDisappearTime = 3.0f;
    public float waitTimer = 0f;

    // Door
    public float detectDoorRange = 2f;
    public float detectDoorRate = 0.5f;
    public float afterDetectDoor;
    [SerializeField] private LayerMask doorLayerMask;
    public LayerMask DoorLayerMask => doorLayerMask;

    // Monologue
    public bool hasBeenSeenByPlayer = false;
    public int firstMonologueNum = 4; 

    protected override void Awake()
    {
        base.Awake();
        nurseZombieAnim = GetComponentInChildren<Animator>();
        nurseZombieAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    protected override void Start()
    {
        base.Start();
        doorLayerMask = LayerMask.GetMask("Interactable");

        Collider nurseCollider = GetComponent<Collider>();
        Collider playerCollider = PlayerTransform.GetComponent<Collider>();
        if (nurseCollider != null && playerCollider != null)
        {
            Physics.IgnoreCollision(nurseCollider, playerCollider);
        }

        fsm = new EnemyStateMachine();
        fsm.ChangeState(new NurseZombieIdleState(this, fsm));
    }

    public bool IsPlayerLookingAtMe()
    {
        Vector3 toNurse = (transform.position - PlayerTransform.position).normalized;
        Vector3 playerforward = PlayerTransform.forward.normalized;
        return Vector3.Dot(toNurse, playerforward) > 0.8f;
    }

    public void MoveTowardsPlayer(float speed)
    {
        if (PlayerTransform == null) return;

        Vector3 direction = (PlayerTransform.position - transform.position).normalized;
        direction.y = 0; // 수평 이동만

        float distance = Vector3.Distance(transform.position, PlayerTransform.position);
        float minDistance = 2.0f;

        if (distance > minDistance)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

}
