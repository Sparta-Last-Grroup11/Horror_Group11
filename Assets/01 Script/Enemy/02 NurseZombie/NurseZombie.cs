using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class NurseZombie : Enemy   // 웃는 천사 기믹: 멈춰있다가 플레이어가 뒤돌면 쫓아오는 간호 좀비
{
    [Header("Components")]
    public NavMeshAgent nurseZombieAgent;
    [HideInInspector] public Rigidbody rb;
    public Animator nurseZombieAnim;
    public LightStateSO lightStateSO;
    public AudioClip nurseZombieChaseClip;
    public AudioClip nurseZombieCatchPlayerClip;
    public CinemachineVirtualCamera nurseZombieVirtualCamera;

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

    [Header("State Tracking")]
    public int blockedByDoorCount = 0;
    public int maxBlockedCount = 2;

    [Header("Audio")]
    public float footStepRate;
    public AudioClip chaseFootStepClip;

    public enum SpawnNursePhase { FirstFloor, SecondFloor }

    protected override void Awake()
    {
        base.Awake();
        nurseZombieAnim = GetComponentInChildren<Animator>();
        nurseZombieAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        doorLayerMask = LayerMask.GetMask("Interactable");
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

    public bool IsPlayerLookingAtMe()  // 카메라가 나를 보고 있는지 판단 (월드 좌표 -> 뷰포트 변환)
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position + Vector3.up * 1.6f);
        bool isInView = viewPos.z > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1;

        if (isInView)
        {
            Vector3 cameraPos = Camera.main.transform.position + Camera.main.transform.forward * 0.1f;
            Vector3 direction = (transform.position + Vector3.up * 1.7f) - cameraPos;
            float distance = direction.magnitude;

            Debug.DrawRay(cameraPos, direction, Color.red, distance);
            if (Physics.Raycast(cameraPos, direction, out RaycastHit hit, distance))
            {
                if (hit.transform == this.transform)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void MoveToSpawnPosition(Vector3 targetPosition, Quaternion targetRotation)  // 좀비를 NavMesh 기준으로 특정 위치로 이동
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, 2f, NavMesh.AllAreas))
        {
            nurseZombieAgent.Warp(hit.position);
            transform.rotation = targetRotation;
        }
    }

    public void ResetEnemy(SpawnNursePhase phase) // 체크포인트 시점에서 좀비 상태 초기화
    {
        nurseZombieVirtualCamera.Priority = 8;
        fsm.ChangeState(nurseZombieIdleState);

        haveSeenPlayer = false;
        nurseZombieAnim.SetBool("Attack", false);

        switch (phase)
        {
            case SpawnNursePhase.FirstFloor:
                MoveToSpawnPosition(new Vector3(-12f, 1.1f, 16.5f), Quaternion.Euler(0, 90f, 0));
                blockedByDoorCount = 0;
                break;

            case SpawnNursePhase.SecondFloor:
                MoveToSpawnPosition(new Vector3(-5.96f, 5.5f, -19.71f), Quaternion.identity);
                blockedByDoorCount = 1;
                break;
        }

        gameObject.SetActive(true);
    }

    public override void ResetEnemy()  // 기본 리셋 (1층 기준)
    {
        ResetEnemy(SpawnNursePhase.FirstFloor);
    }
}
