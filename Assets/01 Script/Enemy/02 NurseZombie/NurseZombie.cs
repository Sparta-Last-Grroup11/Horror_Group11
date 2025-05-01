using UnityEngine;
using UnityEngine.AI;

public class NurseZombie : Enemy   // 웃는 천사 기믹 (멈춰있다가, 플레이어가 뒤돌면 쫓아옴)
{
    [Header("Components")]
    public NavMeshAgent nurseZombieAgent;
    [HideInInspector] public Rigidbody rb;
    public Animator nurseZombieAnim;
    public AudioClip nurseZombieChaseClip;
    public LightStateSO lightStateSO;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float dashSpeed = 6f;
    public float attackRange;
    public float dashTriggerRange = 0.5f;

    [Header("Detection & States")]
    public float detectionRange = 10f;
    public bool hasDetectedPlayer = false;
    public bool hasBeenSeenByPlayer = false;
    public int firstMonologueNum = 4;
    [HideInInspector] public bool hasDashed = false;
    [HideInInspector] public bool canAttack = true;

    [Header("Door Detection")]
    public float detectDoorRange = 2f;
    public float detectDoorRate = 0.5f;
    public float afterDetectDoor;
    [SerializeField] private LayerMask doorLayerMask;
    public LayerMask DoorLayerMask => doorLayerMask;

    [Header("Chase Reset")]
    public float PlayerDisappearTime = 3.0f;
    public float waitTimer = 0f;
    [SerializeField] private Transform spawnPoint;

    protected override void Awake()
    {
        base.Awake();
        nurseZombieAnim = GetComponentInChildren<Animator>();
        nurseZombieAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        doorLayerMask = LayerMask.GetMask("Interactable");

        nurseZombieAgent.updatePosition = true;
        nurseZombieAgent.updateRotation = true;
        nurseZombieAgent.baseOffset = 0f;
        nurseZombieAgent.autoBraking = true;
    }

    protected override void Start()
    {
        base.Start();      
        fsm = new EnemyStateMachine();
        fsm.ChangeState(new NurseZombieIdleState(this, fsm));
    }

    public bool IsPlayerLookingAtMe()
    {
        Vector3 toNurse = (transform.position - PlayerTransform.position);
        Vector3 playerForward = PlayerTransform.forward;

        // y축 제거 (수평 방향만 비교). 플레이어와 간호사좀비 눈높이 차이 조절. 
        toNurse.y = 0;
        playerForward.y = 0;

        toNurse.Normalize();
        playerForward.Normalize();

        float dot = Vector3.Dot(toNurse, playerForward);  // 1에 가까울수록 플레이어 = 몬스터 같은 방향
        float lookThreshold = 0.8f;  // 거의 같은 방향일 때

        return dot > lookThreshold;
    }

    public void MoveTowardsPlayer(float speed, bool isDash = false, bool forceWarp = false)
    {
        if (nurseZombieAgent == null || !nurseZombieAgent.isOnNavMesh) return;

        float distance = Vector3.Distance(transform.position, PlayerTransform.position);
        float minDistance = 0.3f;  //  플레이어와 최소 거리 유지

        if (forceWarp)
        {
            Vector3 toPlayer = (PlayerTransform.position - transform.position).normalized;
            float moveDistance = Mathf.Max(distance - minDistance, 0f);
            Vector3 targetPos = PlayerTransform.position - toPlayer * 1.0f;

            NavMeshHit hit;

            if (NavMesh.SamplePosition(targetPos, out hit, 1f, NavMesh.AllAreas))
            {
                nurseZombieAgent.Warp(hit.position);
            }
            else
            {
                transform.position = targetPos;
            }
            return;
        }

        if (distance > minDistance && nurseZombieAgent.isOnNavMesh)
        {
            nurseZombieAgent.speed = speed;
            nurseZombieAgent.SetDestination(PlayerTransform.position);
        }
        else
        {
            nurseZombieAgent.ResetPath();
        }
    }

    public void MoveToSpawnPosition(Vector3 targetPosition)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, 2f, NavMesh.AllAreas))
        {
            nurseZombieAgent.Warp(hit.position);
        }
        else
        {
            Debug.LogWarning("NavMesh에서 유효한 위치를 찾지 못함");
        }
    }
}
