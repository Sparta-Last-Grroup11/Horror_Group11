using UnityEngine;
using UnityEngine.AI;

public class NurseZombie : Enemy   // 웃는 천사 기믹 (멈춰있다가, 플레이어가 뒤돌면 쫓아옴)
{
    [Header("Components")]
    public NavMeshAgent nurseZombieAgent;
    [HideInInspector] public Rigidbody rb;
    public Animator nurseZombieAnim;
    public LightStateSO lightStateSO;
    public AudioClip nurseZombieChaseClip;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float dashSpeed = 6f;
    public float attackRange;
    public float dashTriggerRange = 0.5f;

    [Header("Detection & States")]
    public float detectionRange = 10f;
    public bool hasBeenSeenByPlayer = false;
    public int firstMonologueNum = 4;
    [HideInInspector] public bool hasDashed = false;

    [Header("Door Detection")]
    public float detectDoorRange = 2f;
    public float detectDoorRate = 0.5f;
    public float afterDetectDoor;
    [SerializeField] private LayerMask doorLayerMask;
    public LayerMask DoorLayerMask => doorLayerMask;

    [Header("Chase Reset")]
    public float PlayerDisappearTime = 3.0f;
    [SerializeField] private Transform spawnPoint;

    [Header("FSM")]
    public NurseZombieIdleState nurseZombieIdleState;
    public NurseZombieChaseState nurseZombieChaseState;
    public NurseZombieAttackState nurseZombieAttackState;

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

        nurseZombieIdleState = new NurseZombieIdleState(this, fsm);
        nurseZombieChaseState = new NurseZombieChaseState(this, fsm);
        nurseZombieAttackState = new NurseZombieAttackState(this, fsm);

        fsm.ChangeState(nurseZombieIdleState);
    }

    public bool IsPlayerLookingAtMe()
    {
        // 계속 이 메서드에서 오류가 떠서, 이 부분은 일단 리팩토링하기 전(원래 제가 썼던 코드)으로 되돌려놓았습니다.
        Vector3 toNurse = (transform.position - PlayerTransform.position);
        Vector3 playerForward = PlayerTransform.forward;

        // y축 제거 (수평 방향만 비교)
        toNurse.y = 0;
        playerForward.y = 0;

        toNurse.Normalize();
        playerForward.Normalize();

        float dot = Vector3.Dot(toNurse, playerForward);
        float lookThreshold = 0.8f;  // 거의 같은 방향일 때

        return dot > lookThreshold;
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
